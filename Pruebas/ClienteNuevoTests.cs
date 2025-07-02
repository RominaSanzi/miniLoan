using di.financiera.datos;
using di.financiera.entidades;
using di.financiera.excepciones;
using di.financiera.utils;
using FluentAssertions;
using System;
using Xunit;

namespace Pruebas
{
    [Collection("DatosColeccion")]
    public class ClienteNuevoTests
    {
        private readonly accesoDatos _accesoDatos;
        public ClienteNuevoTests(DatosFixture fixture)
        {
            _accesoDatos = fixture.Conexion;
        }

        [Fact]
        public void ObtenerCliente_ConIdExistente_ObtieneOk()
        {
            // Arrange - Given
            var cliente = new ClienteNuevo
            {
                accesoDatos = _accesoDatos,
                nombre = "nombre"
            };
            // Act - When
            Action accion = () => cliente.obtenerCliente();

            // Assert - Then
            accion.Should().NotThrow<ClienteNuevoNoEncontradoException>(because: "it exists in the database");
            cliente.Should().NotBeNull();
            cliente.id.Should().NotBe(0).And.Be(1, because: "it exists in the database with said id");
        }

        [Fact]
        public void ObtenerCliente_ConIdInexistente_Lanza()
        {
            // Arrange
            var cliente = new ClienteNuevo
            {
                accesoDatos = _accesoDatos,
                id = -1
            };
            // Act
            Action accion = () => cliente.obtenerCliente();

            // Assert
            accion.Should().ThrowExactly<ClienteNuevoNoEncontradoException>(because: "it is the adequate exception type");
        }

        [Fact]
        public void Crear_ConEntidadValida_CreaOk()
        {
            // Arrange
            var clienteNuevo = new ClienteNuevo
            {
                nombre = "nombre",
                accesoDatos = _accesoDatos
            };

            // Act
            clienteNuevo.crear();

            // Assert 
            var clienteEncontrado = new ClienteNuevo()
            {
                accesoDatos = _accesoDatos
            };

            clienteEncontrado = clienteEncontrado.obtenerCliente();
            clienteEncontrado.Should().NotBeNull(because: "the entity must exist in the database");
            clienteNuevo.id.Should().BeGreaterThan(0, because: "the entity has been created successfully");
            clienteNuevo.nombre.Should().Be("nombre", because: "it has the assigned name");
        }

        [Theory]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("")]
        public void Crear_ConEntidadInvalida_Lanza(string nombre)
        {
            // Arrange
            var clienteNuevo = new ClienteNuevo
            {
                nombre = nombre,
                accesoDatos = _accesoDatos
            };

            // Act
            Action accion = () => clienteNuevo.crear();

            // Assert 
            accion.Should().ThrowExactly<ClienteNuevoNoCreadoException>(because: "values for member 'nombre' are invalid");
        }

        [Fact]
        public void Modificar_ConEntidadValida_ModificaOk()
        {
            // Arrange
            var clienteExistente = new ClienteNuevo()
            {
                id = 1,
                accesoDatos = _accesoDatos
            };
            clienteExistente = clienteExistente.obtenerCliente();
            string nombreAnterior = clienteExistente.nombre;
            clienteExistente.nombre = $"nuevo{Guid.NewGuid()}";
            // Act
            clienteExistente.modificar();

            // Assert
            var clienteEncontrado = new ClienteNuevo()
            {
                id = 1,
                accesoDatos = _accesoDatos
            };
            clienteEncontrado.obtenerCliente();
            clienteEncontrado.Should().NotBeNull(because: "the entity must exist in the database");
            clienteExistente.nombre.Should().NotBe(nombreAnterior, because: "it can't have the same name as before")
            .And.Be(clienteExistente.nombre, because: "it is the new name");
        }

        [Fact]
        public void Eliminar_ConEntidadExistente_EliminaOk()
        {
            // Arrange
            var clienteExistente = new ClienteNuevo
            {
                accesoDatos = _accesoDatos,
                nombre = "nombre"
            };
            clienteExistente.obtenerCliente();

            // Act
            clienteExistente.eliminar();

            // Assert
            Action buscarCliente = () => clienteExistente.obtenerCliente();
            buscarCliente.Should().ThrowExactly<ClienteNuevoNoEncontradoException>(because: "it is the adequate exception type");
        }

        [Fact]
        public void Eliminar_ConEntidadInexistente_Lanza()
        {
            // Arrange
            string cadenaUnica = Guid.NewGuid().ToString();
            var clienteExistente = new ClienteNuevo
            {
                accesoDatos = _accesoDatos,
                nombre = $"{Guid.NewGuid()}"
            };

            // Act
            clienteExistente.eliminar();

            // Assert
            Action buscarCliente = () => clienteExistente.obtenerCliente();
            buscarCliente.Should().ThrowExactly<ClienteNuevoNoEncontradoException>(because: "the entity doesn't exists in the database");
        }
    }
}
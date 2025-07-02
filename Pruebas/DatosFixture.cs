using di.financiera.datos;
using di.financiera.entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pruebas
{
    public class DatosFixture : IDisposable
    {
        public accesoDatos Conexion { get; private set; }

        public DatosFixture()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string stringConexion = config.GetConnectionString("BaseDeDatosPruebas");
            System.Configuration.ConfigurationManager.AppSettings.Set("stringConexion",stringConexion);
            System.Configuration.ConfigurationManager.AppSettings.Set("loguearConexiones","False");
            Conexion = new accesoDatos(stringConexion);
        }

        public void CrearEsquema()
        {
            Conexion.ejecutar("DROP TABLE IF EXISTS clienteNuevo");
            Conexion.ejecutar("CREATE TABLE clienteNuevo (Id INT NOT NULL PRIMARY KEY, Nombre VARCHAR(255) NOT NULL);");
            Conexion.ejecutar("INSERT INTO clienteNuevo (id,nombre) VALUES (1,'nombre')");
        }

        public void Dispose()
        {
            Conexion.ejecutar("DROP TABLE IF EXISTS clienteNuevo");
            Conexion.cerrar();
            GC.SuppressFinalize(this);
        }
    }

    [CollectionDefinition("DatosColeccion")]
    public class DatosColeccion : ICollectionFixture<DatosFixture>
    {
    }
}

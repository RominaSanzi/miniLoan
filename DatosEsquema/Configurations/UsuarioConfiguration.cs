using di.financiera.seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configuraciones
{
    internal class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            HasKey(e => e.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Ignore(e => e.accesoDatos);
            Ignore(e => e.estadoUsuario);
            HasMany(p => p.perfiles)
             .WithMany()
             .Map(m =>
             {
                 m.MapLeftKey("idUsuario");
                 m.MapRightKey("idPerfil");
                 m.ToTable("usuarioperfil"); 
             });

            HasOptional(p => p.usuarioSupervisorAgenda)
                .WithMany()
                .Map(m => m.MapKey("idUsuarioSupervisorAgenda"));

            HasOptional(p => p.usuarioAutorizacion)
                .WithMany()
                .Map(m => m.MapKey("idUsuarioAutorizacion"));

            HasOptional(p => p.tipoUsuarioTarea)
                .WithMany()
                .Map(m => m.MapKey("idTipoUsuarioTarea"));

            HasOptional(p => p.sucursalPuntos)
                .WithMany()
                .Map(m => m.MapKey("idSucursalPuntos"));

            HasOptional(p => p.sectorAutorizacion)
                .WithMany()
                .Map(m => m.MapKey("idSectorAutorizacion"));

            HasOptional(p => p.puntoVentaDgiInmediatoPunitorios)
                .WithMany()
                .Map(m => m.MapKey("idpuntoVentaDgiInmediatoPunitorios"));

            HasOptional(p => p.puntoVentaDgiInmediatoNotaCredito)
                .WithMany()
                .Map(m => m.MapKey("idpuntoVentaDgiInmediatoNotaCredito"));

            HasOptional(p => p.puntoVentaDgiInmediatoInteresesYGastos)
                .WithMany()
                .Map(m => m.MapKey("idpuntoVentaDgiInmediatoInteresesYGastos"));

            HasOptional(p => p.puntoVentaDgiDiferido)
                .WithMany()
                .Map(m => m.MapKey("idpuntoVentaDgiDiferido"));

            HasOptional(p => p.paisVisualizacion)
                .WithMany()
                .Map(m => m.MapKey("idPaisVisualizacion"));

            HasOptional(p => p.nivel)
                .WithMany()
                .Map(m => m.MapKey("idNivel"));

            HasOptional(p => p.medioAccesoSistema)
                .WithMany()
                .Map(m => m.MapKey("idMedioAccesoSistema"));

            HasOptional(p => p.estadoTokenGeneraContrasenia)
                .WithMany()
                .Map(m => m.MapKey("idEstadoTokenGeneraContrasenia"));

            HasOptional(p => p.estado)
                .WithMany()
                .Map(m => m.MapKey("idEstado"));

            HasOptional(p => p.dashboard)
                .WithMany()
                .Map(m => m.MapKey("idDashboard"));

            ToTable("usuario");
        }
    }
}

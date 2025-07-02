using DatosEsquema.Configuraciones;
using DatosEsquema.Configurations;
using DatosEsquema.Models;
using di.financiera.datos;
using di.financiera.entidades;
using di.financiera.seguridad;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LoanContext : DbContext
    {
        public LoanContext() : base("name=StringConexionLoan") 
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LoanContext>());
        }

        public DbSet<Domicilio> Domicilios { get; set; }
        public DbSet<PersonaBD> Personas { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Sexo> Sexos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<TipoParametro> TiposParametro { get; set; }
        public DbSet<TipoDato> TiposDato { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Accion> Acciones { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Nivel> Niveles { get; set; }
        public DbSet<UnidadDeNegocios> UnidadDeNegocios { get; set; }
        public DbSet<EmpresaGrupo> EmpresasGrupo { get; set; }
        public DbSet<GrupoEmpresas> GrupoEmpresas{ get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Pais> Paises { get; set; }


        protected override void OnModelCreating(DbModelBuilder mb)
        {
            mb.Conventions.Remove<PluralizingTableNameConvention>();

            mb.Ignore<Entidad>();
            mb.Ignore<accesoDatos>();

            mb.Configurations.Add(new DomicilioConfiguracion());
            mb.Configurations.Add(new PersonaConfiguracion());
            mb.Configurations.Add(new LocalidadConfiguracion());
            mb.Configurations.Add(new SexoConfiguracion());
            mb.Configurations.Add(new EstadoConfiguracion());
            mb.Configurations.Add(new TipoDocumentoConfiguracion());
            mb.Configurations.Add(new ParametroConfiguration());
            mb.Configurations.Add(new TipoParametroConfiguration());
            mb.Configurations.Add(new TipoDatoConfiguration());
            mb.Configurations.Add(new UsuarioConfiguration());
            mb.Configurations.Add(new AccionConfiguration());
            mb.Configurations.Add(new MenuConfiguration());
            mb.Configurations.Add(new NivelConfiguration());
            mb.Configurations.Add(new EmpresaGrupoConfiguration());
            mb.Configurations.Add(new GrupoEmpresasConfiguration());
            mb.Configurations.Add(new SucursalConfiguration());
            mb.Configurations.Add(new UnidadDeNogociosConfiguration());

            base.OnModelCreating(mb);
        }
    }
}

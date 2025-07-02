using di.financiera.seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configurations
{
    internal class RolConfiguration : EntityTypeConfiguration<Rol>
    {
        public RolConfiguration()
        {
            HasKey(e => e.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Ignore(e => e.accesoDatos);
            HasOptional(r => r.usuario)
                .WithMany()
                .Map(m => m.MapKey("idUsuario"));
            ToTable("rol");
        }
    }
}

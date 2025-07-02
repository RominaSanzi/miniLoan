using di.financiera.seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configurations
{
    public class SucursalConfiguration : EntityTypeConfiguration<Sucursal>
    {
        public SucursalConfiguration()
        {
            // Ignore(e => e.accesoDatos);
            HasKey(e => e.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            HasOptional(p => p.domicilio)
                .WithMany()
                .Map(m => m.MapKey("idDomicilio"));
        }
    }
}

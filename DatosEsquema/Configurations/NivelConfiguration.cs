using di.financiera.seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configurations
{
    public class NivelConfiguration : EntityTypeConfiguration<Nivel>
    {
        public NivelConfiguration()
        {
            Ignore(e => e.padre);
            Ignore(e => e.accesoDatos);
            HasKey(e => e.id).Property(e => e.id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            ToTable("nivel");
        }
    }
}

using di.financiera.seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configurations
{
    public class GrupoEmpresasConfiguration : EntityTypeConfiguration<GrupoEmpresas>
    {
        public GrupoEmpresasConfiguration()
        {
            HasKey(e => e.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            // Ignore(e => e.accesoDatos);
            ToTable("grupoempresas");
        }
    }
}

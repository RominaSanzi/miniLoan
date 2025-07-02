using di.financiera.seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configurations
{
    public class EmpresaGrupoConfiguration : EntityTypeConfiguration<EmpresaGrupo>
    {
        public EmpresaGrupoConfiguration()
        {
            HasKey(e => e.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            // Ignore(e => e.accesoDatos);
            HasOptional(e => e.domicilio)
                .WithMany()
                .Map(m => m.MapKey("idDomicilio"));
            ToTable("empresagrupo");
        }
    }
}

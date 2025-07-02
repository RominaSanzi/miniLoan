using di.financiera.entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configuraciones
{
    internal class TipoDocumentoConfiguracion : EntityTypeConfiguration<TipoDocumento>
    {
        public TipoDocumentoConfiguracion()
        {
            HasKey(t => t.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Ignore(e => e.accesoDatos);
            ToTable("tipodocumento");
        }
    }
}

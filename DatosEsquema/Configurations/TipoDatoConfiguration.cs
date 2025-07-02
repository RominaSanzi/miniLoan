using di.financiera.entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configuraciones
{
    internal class TipoDatoConfiguration : EntityTypeConfiguration<TipoDato>
    {
        public TipoDatoConfiguration()
        {
            // Ignore(e => e.accesoDatos);
            HasKey(e => e.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            ToTable("tipodato");
        }
    }
}

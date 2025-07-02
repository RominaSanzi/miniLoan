using di.financiera.entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configuraciones
{
    public class TipoParametroConfiguration : EntityTypeConfiguration<TipoParametro>
    {
        public TipoParametroConfiguration()
        {
            HasKey(e => e.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Ignore(e => e.accesoDatos);
            ToTable("tipoparametro");
        }
    }
}

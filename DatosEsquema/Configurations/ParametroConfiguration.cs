using di.financiera.entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configuraciones
{
    public class ParametroConfiguration : EntityTypeConfiguration<Parametro>
    {
        public ParametroConfiguration()
        {
            HasKey(e => e.id).Property(e => e.id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Ignore(e => e.accesoDatos);

            HasOptional(p => p.tipoParametro)
                .WithMany()
                .Map(m => m.MapKey("idTipoParametro"));

            HasOptional(p => p.tipoDato)
                .WithMany()
                .Map(m => m.MapKey("idTipoDato"));
            ToTable("parametro");
        }
    }
}

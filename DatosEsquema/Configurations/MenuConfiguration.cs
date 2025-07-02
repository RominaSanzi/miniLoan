using di.financiera.seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configuraciones
{
    public class MenuConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        {
            Ignore(e => e.accesoDatos);
            HasKey(e => e.id).Property(e => e.id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            HasOptional(d => d.accion)
                .WithMany()
                .Map(m => m.MapKey("idAccion"));
            ToTable("menu");
        }
    }
}

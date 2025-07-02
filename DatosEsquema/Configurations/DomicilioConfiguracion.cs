using di.financiera.entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configuraciones
{
    public class DomicilioConfiguracion : EntityTypeConfiguration<Domicilio>
    {
        public DomicilioConfiguracion()
        {
            HasKey(d => d.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Ignore(e => e.accesoDatos);
            HasOptional(d => d.localidad)
                .WithMany()
                .Map(m => m.MapKey("idLocalidad"));
            ToTable("domicilio");
        }
    }
}

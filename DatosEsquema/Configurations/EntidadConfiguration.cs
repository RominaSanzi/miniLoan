using di.financiera.datos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configurations
{
    public class EntidadConfiguration : EntityTypeConfiguration<Entidad>
    {
        public EntidadConfiguration()
        {
            Ignore(e => e.accesoDatos);
        }
    }
}

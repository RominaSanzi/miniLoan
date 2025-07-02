using di.financiera.datos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configurations
{
    public class AccesoDatosConfiguration : EntityTypeConfiguration<accesoDatos>
    {
        public AccesoDatosConfiguration()
        {
            Ignore(e => e.activeTransaction);
            Ignore(e=> e.conexion);
            Ignore(e=> e.activeTransactionSQLServer);
            Ignore(e=> e.conexionSQLServer);
            Ignore(e=> e.baseDeDatos);
        }
    }
}

using DatosEsquema.Models;
using di.financiera.entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEsquema.Configuraciones
{
    internal class PersonaConfiguracion : EntityTypeConfiguration<PersonaBD>
    {
        public PersonaConfiguracion()
        {
            HasKey(p => p.id)
                .Property(e => e.id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            // Ignore(e => e.accesoDatos);
            HasOptional(p => p.tipoDocumento)
                .WithMany()
                .Map(m => m.MapKey("idTipoDocumento"));

            HasOptional(p => p.domicilio)
                .WithMany()
                .Map(m => m.MapKey("idDomicilio"));


            HasOptional(p => p.estado)
                .WithMany()
                .Map(m => m.MapKey("idEstado"));


            HasOptional(p => p.sexo)
                .WithMany()
                .Map(m => m.MapKey("idSexo"));

            ToTable("persona");
        }
    }
}

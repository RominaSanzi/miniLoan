using di.financiera.datos;
using di.financiera.entidades;
using di.financiera.seguridad;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace DatosEsquema
{
    public class LoanInitializer
    {
        private readonly DbContext _context;
        public LoanInitializer(DbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            CreateSchema();
            Seed();
        }

        private void CreateSchema()
        {
            if(!_context.Database.Exists())
            {
                _context.Database.Create();
            }
        }

        private void Seed()
        {
            //var menuLoan = new MenuSimple()
            //{
            //    id=42,
            //    nombre = "LOAN",
            //    final = false,
            //    accion = null,
            //    nivel = "top",
            //    icono = "NOW-ui-icons shopping_tag-content"
            //};
            //var menuOperaciones = new MenuCompuesto()
            //{
            //    nombre = "Operaciones",
            //    final = false,
            //    idPadre = 42,
            //    accion = null,
            //    nivel = "top",
            //    icono = "NOW-ui-icons business_globe"
            //};
            //_context.Set<Menu>()
            //    .AddRange(new List<Menu>() { menuLoan, menuOperaciones });

            //_context.SaveChanges();
            var conexion = new accesoDatos();

            var sexos = new List<Sexo>()
            {
                new Sexo()
                {
                    id = 1,
                    descripcion="GENERICO"
                },
                new Sexo()
                {
                    id = 2,
                    descripcion="MASCULINO"
                },                
                new Sexo()
                {
                    id = 3,
                    descripcion="FEMENINO"
                }
            };

            sexos.ForEach(s =>
            {
                s.accesoDatos = conexion;
                s.crear();
            });

            var paises = JsonSerializer.Deserialize<List<Pais>>(@"[ 
{ 
""codigo"":""AX"",
""nombre"":""Islas Gland"",
""habilitado"":false
},
{ 
""codigo"":""AL"",
""nombre"":""Albania"",
""habilitado"":false
},
{ 
""codigo"":""DE"",
""nombre"":""Alemania"",
""habilitado"":false
},
{ 
""codigo"":""AD"",
""nombre"":""Andorra"",
""habilitado"":false
},
{ 
""codigo"":""AO"",
""nombre"":""Angola"",
""habilitado"":false
},
{ 
""codigo"":""AI"",
""nombre"":""Anguilla"",
""habilitado"":false
},
{ 
""codigo"":""AQ"",
""nombre"":""Antártida"",
""habilitado"":false
},
{ 
""codigo"":""AG"",
""nombre"":""Antigua y Barbuda"",
""habilitado"":false
},
{ 
""codigo"":""AN"",
""nombre"":""Antillas Holandesas"",
""habilitado"":false
},
{ 
""codigo"":""SA"",
""nombre"":""Arabia Saudí"",
""habilitado"":false
},
{ 
""codigo"":""DZ"",
""nombre"":""Argelia"",
""habilitado"":false
},
{ 
""codigo"":""AR"",
""nombre"":""Argentina"",
""habilitado"":true
},
{ 
""codigo"":""AM"",
""nombre"":""Armenia"",
""habilitado"":false
},
{ 
""codigo"":""AW"",
""nombre"":""Aruba"",
""habilitado"":false
},
{ 
""codigo"":""AU"",
""nombre"":""Australia"",
""habilitado"":false
},
{ 
""codigo"":""AT"",
""nombre"":""Austria"",
""habilitado"":false
},
{ 
""codigo"":""AZ"",
""nombre"":""Azerbaiyán"",
""habilitado"":false
},
{ 
""codigo"":""BS"",
""nombre"":""Bahamas"",
""habilitado"":false
},
{ 
""codigo"":""BH"",
""nombre"":""Bahréin"",
""habilitado"":false
},
{ 
""codigo"":""BD"",
""nombre"":""Bangladesh"",
""habilitado"":false
},
{ 
""codigo"":""BB"",
""nombre"":""Barbados"",
""habilitado"":false
},
{ 
""codigo"":""BY"",
""nombre"":""Bielorrusia"",
""habilitado"":false
},
{ 
""codigo"":""BE"",
""nombre"":""Bélgica"",
""habilitado"":false
},
{ 
""codigo"":""BZ"",
""nombre"":""Belice"",
""habilitado"":false
},
{ 
""codigo"":""BJ"",
""nombre"":""Benin"",
""habilitado"":false
},
{ 
""codigo"":""BM"",
""nombre"":""Bermudas"",
""habilitado"":false
},
{ 
""codigo"":""BT"",
""nombre"":""Bhután"",
""habilitado"":false
},
{ 
""codigo"":""BO"",
""nombre"":""Bolivia"",
""habilitado"":false
},
{ 
""codigo"":""BA"",
""nombre"":""Bosnia y Herzegovina"",
""habilitado"":false
},
{ 
""codigo"":""BW"",
""nombre"":""Botsuana"",
""habilitado"":false
},
{ 
""codigo"":""BV"",
""nombre"":""Isla Bouvet"",
""habilitado"":false
},
{ 
""codigo"":""BR"",
""nombre"":""Brasil"",
""habilitado"":false
},
{ 
""codigo"":""BN"",
""nombre"":""Brunéi"",
""habilitado"":false
},
{ 
""codigo"":""BG"",
""nombre"":""Bulgaria"",
""habilitado"":false
},
{ 
""codigo"":""BF"",
""nombre"":""Burkina Faso"",
""habilitado"":false
},
{ 
""codigo"":""BI"",
""nombre"":""Burundi"",
""habilitado"":false
},
{ 
""codigo"":""CV"",
""nombre"":""Cabo Verde"",
""habilitado"":false
},
{ 
""codigo"":""KY"",
""nombre"":""Islas Caimán"",
""habilitado"":false
},
{ 
""codigo"":""KH"",
""nombre"":""Camboya"",
""habilitado"":false
},
{ 
""codigo"":""CM"",
""nombre"":""Camerún"",
""habilitado"":false
},
{ 
""codigo"":""CA"",
""nombre"":""Canadá"",
""habilitado"":false
},
{ 
""codigo"":""CF"",
""nombre"":""República Centroafricana"",
""habilitado"":false
},
{ 
""codigo"":""TD"",
""nombre"":""Chad"",
""habilitado"":false
},
{ 
""codigo"":""CZ"",
""nombre"":""República Checa"",
""habilitado"":false
},
{ 
""codigo"":""CL"",
""nombre"":""Chile"",
""habilitado"":false
},
{ 
""codigo"":""CN"",
""nombre"":""China"",
""habilitado"":false
},
{ 
""codigo"":""CY"",
""nombre"":""Chipre"",
""habilitado"":false
},
{ 
""codigo"":""CX"",
""nombre"":""Isla de Navidad"",
""habilitado"":false
},
{ 
""codigo"":""VA"",
""nombre"":""Ciudad del Vaticano"",
""habilitado"":false
},
{ 
""codigo"":""CC"",
""nombre"":""Islas Cocos"",
""habilitado"":false
},
{ 
""codigo"":""CO"",
""nombre"":""Colombia"",
""habilitado"":false
},
{ 
""codigo"":""KM"",
""nombre"":""Comoras"",
""habilitado"":false
},
{ 
""codigo"":""CD"",
""nombre"":""República Democrática del Congo"",
""habilitado"":false
},
{ 
""codigo"":""CG"",
""nombre"":""Congo"",
""habilitado"":false
},
{ 
""codigo"":""CK"",
""nombre"":""Islas Cook"",
""habilitado"":false
},
{ 
""codigo"":""KP"",
""nombre"":""Corea del Norte"",
""habilitado"":false
},
{ 
""codigo"":""KR"",
""nombre"":""Corea del Sur"",
""habilitado"":false
},
{ 
""codigo"":""CI"",
""nombre"":""Costa de Marfil"",
""habilitado"":false
},
{ 
""codigo"":""CR"",
""nombre"":""Costa Rica"",
""habilitado"":false
},
{ 
""codigo"":""HR"",
""nombre"":""Croacia"",
""habilitado"":false
},
{ 
""codigo"":""CU"",
""nombre"":""Cuba"",
""habilitado"":false
},
{ 
""codigo"":""DK"",
""nombre"":""Dinamarca"",
""habilitado"":false
},
{ 
""codigo"":""DM"",
""nombre"":""Dominica"",
""habilitado"":false
},
{ 
""codigo"":""DO"",
""nombre"":""República Dominicana"",
""habilitado"":false
},
{ 
""codigo"":""EC"",
""nombre"":""Ecuador"",
""habilitado"":false
},
{ 
""codigo"":""EG"",
""nombre"":""Egipto"",
""habilitado"":false
},
{ 
""codigo"":""SV"",
""nombre"":""El Salvador"",
""habilitado"":false
},
{ 
""codigo"":""AE"",
""nombre"":""Emiratos Árabes Unidos"",
""habilitado"":false
},
{ 
""codigo"":""ER"",
""nombre"":""Eritrea"",
""habilitado"":false
},
{ 
""codigo"":""SK"",
""nombre"":""Eslovaquia"",
""habilitado"":false
},
{ 
""codigo"":""SI"",
""nombre"":""Eslovenia"",
""habilitado"":false
},
{ 
""codigo"":""ES"",
""nombre"":""España"",
""habilitado"":false
},
{ 
""codigo"":""UM"",
""nombre"":""Islas ultramarinas de Estados Unidos"",
""habilitado"":false
},
{ 
""codigo"":""US"",
""nombre"":""Estados Unidos"",
""habilitado"":false
},
{ 
""codigo"":""EE"",
""nombre"":""Estonia"",
""habilitado"":false
},
{ 
""codigo"":""ET"",
""nombre"":""Etiopía"",
""habilitado"":false
},
{ 
""codigo"":""FO"",
""nombre"":""Islas Feroe"",
""habilitado"":false
},
{ 
""codigo"":""PH"",
""nombre"":""Filipinas"",
""habilitado"":false
},
{ 
""codigo"":""FI"",
""nombre"":""Finlandia"",
""habilitado"":false
},
{ 
""codigo"":""FJ"",
""nombre"":""Fiyi"",
""habilitado"":false
},
{ 
""codigo"":""FR"",
""nombre"":""Francia"",
""habilitado"":false
},
{ 
""codigo"":""GA"",
""nombre"":""Gabón"",
""habilitado"":false
},
{ 
""codigo"":""GM"",
""nombre"":""Gambia"",
""habilitado"":false
},
{ 
""codigo"":""GE"",
""nombre"":""Georgia"",
""habilitado"":false
},
{ 
""codigo"":""GS"",
""nombre"":""Islas Georgias del Sur y Sandwich del Sur"",
""habilitado"":false
},
{ 
""codigo"":""GH"",
""nombre"":""Ghana"",
""habilitado"":false
},
{ 
""codigo"":""GI"",
""nombre"":""Gibraltar"",
""habilitado"":false
},
{ 
""codigo"":""GD"",
""nombre"":""Granada"",
""habilitado"":false
},
{ 
""codigo"":""GR"",
""nombre"":""Grecia"",
""habilitado"":false
},
{ 
""codigo"":""GL"",
""nombre"":""Groenlandia"",
""habilitado"":false
},
{ 
""codigo"":""GP"",
""nombre"":""Guadalupe"",
""habilitado"":false
},
{ 
""codigo"":""GU"",
""nombre"":""Guam"",
""habilitado"":false
},
{ 
""codigo"":""GT"",
""nombre"":""Guatemala"",
""habilitado"":false
},
{ 
""codigo"":""GF"",
""nombre"":""Guayana Francesa"",
""habilitado"":false
},
{ 
""codigo"":""GN"",
""nombre"":""Guinea"",
""habilitado"":false
},
{ 
""codigo"":""GQ"",
""nombre"":""Guinea Ecuatorial"",
""habilitado"":false
},
{ 
""codigo"":""GW"",
""nombre"":""Guinea-Bissau"",
""habilitado"":false
},
{ 
""codigo"":""GY"",
""nombre"":""Guyana"",
""habilitado"":false
},
{ 
""codigo"":""HT"",
""nombre"":""Haití"",
""habilitado"":false
},
{ 
""codigo"":""HM"",
""nombre"":""Islas Heard y McDonald"",
""habilitado"":false
},
{ 
""codigo"":""HN"",
""nombre"":""Honduras"",
""habilitado"":false
},
{ 
""codigo"":""HK"",
""nombre"":""Hong Kong"",
""habilitado"":false
},
{ 
""codigo"":""HU"",
""nombre"":""Hungría"",
""habilitado"":false
},
{ 
""codigo"":""IN"",
""nombre"":""India"",
""habilitado"":false
},
{ 
""codigo"":""ID"",
""nombre"":""Indonesia"",
""habilitado"":false
},
{ 
""codigo"":""IR"",
""nombre"":""Irán"",
""habilitado"":false
},
{ 
""codigo"":""IQ"",
""nombre"":""Iraq"",
""habilitado"":false
},
{ 
""codigo"":""IE"",
""nombre"":""Irlanda"",
""habilitado"":false
},
{ 
""codigo"":""IS"",
""nombre"":""Islandia"",
""habilitado"":false
},
{ 
""codigo"":""IL"",
""nombre"":""Israel"",
""habilitado"":false
},
{ 
""codigo"":""IT"",
""nombre"":""Italia"",
""habilitado"":false
},
{ 
""codigo"":""JM"",
""nombre"":""Jamaica"",
""habilitado"":false
},
{ 
""codigo"":""JP"",
""nombre"":""Japón"",
""habilitado"":false
},
{ 
""codigo"":""JO"",
""nombre"":""Jordania"",
""habilitado"":false
},
{ 
""codigo"":""KZ"",
""nombre"":""Kazajstán"",
""habilitado"":false
},
{ 
""codigo"":""KE"",
""nombre"":""Kenia"",
""habilitado"":false
},
{ 
""codigo"":""KG"",
""nombre"":""Kirguistán"",
""habilitado"":false
},
{ 
""codigo"":""KI"",
""nombre"":""Kiribati"",
""habilitado"":false
},
{ 
""codigo"":""KW"",
""nombre"":""Kuwait"",
""habilitado"":false
},
{ 
""codigo"":""LA"",
""nombre"":""Laos"",
""habilitado"":false
},
{ 
""codigo"":""LS"",
""nombre"":""Lesotho"",
""habilitado"":false
},
{ 
""codigo"":""LV"",
""nombre"":""Letonia"",
""habilitado"":false
},
{ 
""codigo"":""LB"",
""nombre"":""Líbano"",
""habilitado"":false
},
{ 
""codigo"":""LR"",
""nombre"":""Liberia"",
""habilitado"":false
},
{ 
""codigo"":""LY"",
""nombre"":""Libia"",
""habilitado"":false
},
{ 
""codigo"":""LI"",
""nombre"":""Liechtenstein"",
""habilitado"":false
},
{ 
""codigo"":""LT"",
""nombre"":""Lituania"",
""habilitado"":false
},
{ 
""codigo"":""LU"",
""nombre"":""Luxemburgo"",
""habilitado"":false
},
{ 
""codigo"":""MO"",
""nombre"":""Macao"",
""habilitado"":false
},
{ 
""codigo"":""MK"",
""nombre"":""ARY Macedonia"",
""habilitado"":false
},
{ 
""codigo"":""MG"",
""nombre"":""Madagascar"",
""habilitado"":false
},
{ 
""codigo"":""MY"",
""nombre"":""Malasia"",
""habilitado"":false
},
{ 
""codigo"":""MW"",
""nombre"":""Malawi"",
""habilitado"":false
},
{ 
""codigo"":""MV"",
""nombre"":""Maldivas"",
""habilitado"":false
},
{ 
""codigo"":""ML"",
""nombre"":""Malí"",
""habilitado"":false
},
{ 
""codigo"":""MT"",
""nombre"":""Malta"",
""habilitado"":false
},
{ 
""codigo"":""FK"",
""nombre"":""Islas Malvinas"",
""habilitado"":false
},
{ 
""codigo"":""MP"",
""nombre"":""Islas Marianas del Norte"",
""habilitado"":false
},
{ 
""codigo"":""MA"",
""nombre"":""Marruecos"",
""habilitado"":false
},
{ 
""codigo"":""MH"",
""nombre"":""Islas Marshall"",
""habilitado"":false
},
{ 
""codigo"":""MQ"",
""nombre"":""Martinica"",
""habilitado"":false
},
{ 
""codigo"":""MU"",
""nombre"":""Mauricio"",
""habilitado"":false
},
{ 
""codigo"":""MR"",
""nombre"":""Mauritania"",
""habilitado"":false
},
{ 
""codigo"":""YT"",
""nombre"":""Mayotte"",
""habilitado"":false
},
{ 
""codigo"":""MX"",
""nombre"":""México"",
""habilitado"":false
},
{ 
""codigo"":""FM"",
""nombre"":""Micronesia"",
""habilitado"":false
},
{ 
""codigo"":""MD"",
""nombre"":""Moldavia"",
""habilitado"":false
},
{ 
""codigo"":""MC"",
""nombre"":""Mónaco"",
""habilitado"":false
},
{ 
""codigo"":""MN"",
""nombre"":""Mongolia"",
""habilitado"":false
},
{ 
""codigo"":""MS"",
""nombre"":""Montserrat"",
""habilitado"":false
},
{ 
""codigo"":""MZ"",
""nombre"":""Mozambique"",
""habilitado"":false
},
{ 
""codigo"":""MM"",
""nombre"":""Myanmar"",
""habilitado"":false
},
{ 
""codigo"":""NA"",
""nombre"":""Namibia"",
""habilitado"":false
},
{ 
""codigo"":""NR"",
""nombre"":""Nauru"",
""habilitado"":false
},
{ 
""codigo"":""NP"",
""nombre"":""Nepal"",
""habilitado"":false
},
{ 
""codigo"":""NI"",
""nombre"":""Nicaragua"",
""habilitado"":false
},
{ 
""codigo"":""NE"",
""nombre"":""Níger"",
""habilitado"":false
},
{ 
""codigo"":""NG"",
""nombre"":""Nigeria"",
""habilitado"":false
},
{ 
""codigo"":""NU"",
""nombre"":""Niue"",
""habilitado"":false
},
{ 
""codigo"":""NF"",
""nombre"":""Isla Norfolk"",
""habilitado"":false
},
{ 
""codigo"":""NO"",
""nombre"":""Noruega"",
""habilitado"":false
},
{ 
""codigo"":""NC"",
""nombre"":""Nueva Caledonia"",
""habilitado"":false
},
{ 
""codigo"":""NZ"",
""nombre"":""Nueva Zelanda"",
""habilitado"":false
},
{ 
""codigo"":""OM"",
""nombre"":""Omán"",
""habilitado"":false
},
{ 
""codigo"":""NL"",
""nombre"":""Países Bajos"",
""habilitado"":false
},
{ 
""codigo"":""PK"",
""nombre"":""Pakistán"",
""habilitado"":false
},
{ 
""codigo"":""PW"",
""nombre"":""Palau"",
""habilitado"":false
},
{ 
""codigo"":""PS"",
""nombre"":""Palestina"",
""habilitado"":false
},
{ 
""codigo"":""PA"",
""nombre"":""Panamá"",
""habilitado"":false
},
{ 
""codigo"":""PG"",
""nombre"":""Papúa Nueva Guinea"",
""habilitado"":false
},
{ 
""codigo"":""PY"",
""nombre"":""Paraguay"",
""habilitado"":false
},
{ 
""codigo"":""PE"",
""nombre"":""Perú"",
""habilitado"":false
},
{ 
""codigo"":""PN"",
""nombre"":""Islas Pitcairn"",
""habilitado"":false
},
{ 
""codigo"":""PF"",
""nombre"":""Polinesia Francesa"",
""habilitado"":false
},
{ 
""codigo"":""PL"",
""nombre"":""Polonia"",
""habilitado"":false
},
{ 
""codigo"":""PT"",
""nombre"":""Portugal"",
""habilitado"":false
},
{ 
""codigo"":""PR"",
""nombre"":""Puerto Rico"",
""habilitado"":false
},
{ 
""codigo"":""QA"",
""nombre"":""Qatar"",
""habilitado"":false
},
{ 
""codigo"":""GB"",
""nombre"":""Reino Unido"",
""habilitado"":false
},
{ 
""codigo"":""RE"",
""nombre"":""Reunión"",
""habilitado"":false
},
{ 
""codigo"":""RW"",
""nombre"":""Ruanda"",
""habilitado"":false
},
{ 
""codigo"":""RO"",
""nombre"":""Rumania"",
""habilitado"":false
},
{ 
""codigo"":""RU"",
""nombre"":""Rusia"",
""habilitado"":false
},
{ 
""codigo"":""EH"",
""nombre"":""Sahara Occidental"",
""habilitado"":false
},
{ 
""codigo"":""SB"",
""nombre"":""Islas Salomón"",
""habilitado"":false
},
{ 
""codigo"":""WS"",
""nombre"":""Samoa"",
""habilitado"":false
},
{ 
""codigo"":""AS"",
""nombre"":""Samoa Americana"",
""habilitado"":false
},
{ 
""codigo"":""KN"",
""nombre"":""San Cristóbal y Nevis"",
""habilitado"":false
},
{ 
""codigo"":""SM"",
""nombre"":""San Marino"",
""habilitado"":false
},
{ 
""codigo"":""PM"",
""nombre"":""San Pedro y Miquelón"",
""habilitado"":false
},
{ 
""codigo"":""VC"",
""nombre"":""San Vicente y las Granadinas"",
""habilitado"":false
},
{ 
""codigo"":""SH"",
""nombre"":""Santa Helena"",
""habilitado"":false
},
{ 
""codigo"":""LC"",
""nombre"":""Santa Lucía"",
""habilitado"":false
},
{ 
""codigo"":""ST"",
""nombre"":""Santo Tomé y Príncipe"",
""habilitado"":false
},
{ 
""codigo"":""SN"",
""nombre"":""Senegal"",
""habilitado"":false
},
{ 
""codigo"":""CS"",
""nombre"":""Serbia y Montenegro"",
""habilitado"":false
},
{ 
""codigo"":""SC"",
""nombre"":""Seychelles"",
""habilitado"":false
},
{ 
""codigo"":""SL"",
""nombre"":""Sierra Leona"",
""habilitado"":false
},
{ 
""codigo"":""SG"",
""nombre"":""Singapur"",
""habilitado"":false
},
{ 
""codigo"":""SY"",
""nombre"":""Siria"",
""habilitado"":false
},
{ 
""codigo"":""SO"",
""nombre"":""Somalia"",
""habilitado"":false
},
{ 
""codigo"":""LK"",
""nombre"":""Sri Lanka"",
""habilitado"":false
},
{ 
""codigo"":""SZ"",
""nombre"":""Suazilandia"",
""habilitado"":false
},
{ 
""codigo"":""ZA"",
""nombre"":""Sudáfrica"",
""habilitado"":false
},
{ 
""codigo"":""SD"",
""nombre"":""Sudán"",
""habilitado"":false
},
{ 
""codigo"":""SE"",
""nombre"":""Suecia"",
""habilitado"":false
},
{ 
""codigo"":""CH"",
""nombre"":""Suiza"",
""habilitado"":false
},
{ 
""codigo"":""SR"",
""nombre"":""Surinam"",
""habilitado"":false
},
{ 
""codigo"":""SJ"",
""nombre"":""Svalbard y Jan Mayen"",
""habilitado"":false
},
{ 
""codigo"":""TH"",
""nombre"":""Tailandia"",
""habilitado"":false
},
{ 
""codigo"":""TW"",
""nombre"":""Taiwán"",
""habilitado"":false
},
{ 
""codigo"":""TZ"",
""nombre"":""Tanzania"",
""habilitado"":false
},
{ 
""codigo"":""TJ"",
""nombre"":""Tayikistán"",
""habilitado"":false
},
{ 
""codigo"":""IO"",
""nombre"":""Territorio Británico del Océano Índico"",
""habilitado"":false
},
{ 
""codigo"":""TF"",
""nombre"":""Territorios Australes Franceses"",
""habilitado"":false
},
{ 
""codigo"":""TL"",
""nombre"":""Timor Oriental"",
""habilitado"":false
},
{ 
""codigo"":""TG"",
""nombre"":""Togo"",
""habilitado"":false
},
{ 
""codigo"":""TK"",
""nombre"":""Tokelau"",
""habilitado"":false
},
{ 
""codigo"":""TO"",
""nombre"":""Tonga"",
""habilitado"":false
},
{ 
""codigo"":""TT"",
""nombre"":""Trinidad y Tobago"",
""habilitado"":false
},
{ 
""codigo"":""TN"",
""nombre"":""Túnez"",
""habilitado"":false
},
{ 
""codigo"":""TC"",
""nombre"":""Islas Turcas y Caicos"",
""habilitado"":false
},
{ 
""codigo"":""TM"",
""nombre"":""Turkmenistán"",
""habilitado"":false
},
{ 
""codigo"":""TR"",
""nombre"":""Turquía"",
""habilitado"":false
},
{ 
""codigo"":""TV"",
""nombre"":""Tuvalu"",
""habilitado"":false
},
{ 
""codigo"":""UA"",
""nombre"":""Ucrania"",
""habilitado"":false
},
{ 
""codigo"":""UG"",
""nombre"":""Uganda"",
""habilitado"":false
},
{ 
""codigo"":""UY"",
""nombre"":""Uruguay"",
""habilitado"":false
},
{ 
""codigo"":""UZ"",
""nombre"":""Uzbekistán"",
""habilitado"":false
},
{ 
""codigo"":""VU"",
""nombre"":""Vanuatu"",
""habilitado"":false
},
{ 
""codigo"":""VE"",
""nombre"":""Venezuela"",
""habilitado"":false
},
{ 
""codigo"":""VN"",
""nombre"":""Vietnam"",
""habilitado"":false
},
{ 
""codigo"":""VG"",
""nombre"":""Islas Vírgenes Británicas"",
""habilitado"":false
},
{ 
""codigo"":""VI"",
""nombre"":""Islas Vírgenes de los Estados Unidos"",
""habilitado"":false
},
{ 
""codigo"":""WF"",
""nombre"":""Wallis y Futuna"",
""habilitado"":false
},
{ 
""codigo"":""YE"",
""nombre"":""Yemen"",
""habilitado"":false
},
{ 
""codigo"":""DJ"",
""nombre"":""Yibuti"",
""habilitado"":false
},
{ 
""codigo"":""ZM"",
""nombre"":""Zambia"",
""habilitado"":false
},
{ 
""codigo"":""ZW"",
""nombre"":""Zimbabue"",
""habilitado"":false
}]");

            paises.ForEach(p => {
                p.accesoDatos = conexion;
                p.crear();
            });

            var adminGen = new Usuario() { 
                nombre = "ADMINGEN",
                password = "vento2010",
                login = "ADMINGEN",
                trabajaConAgenda = true,
                autorizaSolicitud = true,
                controlaRemito = true,
                bandejaWelcome = true,
                usuarioFront = true,
                paisVisualizacion = paises.FirstOrDefault(p => p.id == 13),
                estado = new Alta()
            };

            adminGen.accesoDatos = conexion;
            adminGen.crear();

            var localidades = JsonSerializer.Deserialize<List<Localidad>>(@"[ 
{ 
""Descripcion"":""BARRANQUERAS"",
""CodigoPostal"":""3480""
},
{ 
""Descripcion"":""COLONIA CAIMAN"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""COLONIA SAN ANTONIO"",
""CodigoPostal"":""3302""
},
{ 
""Descripcion"":""INFANTE"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""LA ANGELA"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""LA PACHINA"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""LAPACHO"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""LOMAS SAN JUAN"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""LORETO"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""OMBU"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""PALMA SOLA"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""SAN MIGUEL"",
""CodigoPostal"":""3231""
},
{ 
""Descripcion"":""SAN NICOLAS"",
""CodigoPostal"":""3420""
},
{ 
""Descripcion"":""SAN SEBASTIAN"",
""CodigoPostal"":""3448""
},
{ 
""Descripcion"":""SANTA ISABEL"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""SILVERO CUE"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""TACUARAL"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""TACUAREMBO"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""TAPE RATI"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""TIMBO PASO"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""VERON CUE"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""YATAITY POI"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""YATAITI SATA"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""YTA PASO"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""YUQUERI"",
""CodigoPostal"":""3470""
},
{ 
""Descripcion"":""ARROYO BALMACEDA"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""BASTIDORES"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""CARANDAITI"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""CARRETA PASO"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""CASUALIDAD"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""COLONIA"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""COLONIA LA UNION"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""COLONIA MADARIAGA"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""CURUPAYTI"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""CURUZU LAUREL"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""IPACARAPA"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""LOS SAUCES"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""MBOI CUA"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""CATALAN CUE"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""OBRAJE DEL VASCO"",
""CodigoPostal"":""3403""
},
{ 
""Descripcion"":""COLONIA GAIMAN"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""ITA PASO"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""COSTA CENISAL"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""MONTAÑA"",
""CodigoPostal"":""3485""
},
{ 
""Descripcion"":""ÑURUGUAY"",
""CodigoPostal"":""3483""
},
{ 
""Descripcion"":""BELLA VISTA"",
""CodigoPostal"":""1661""
},
{ 
""Descripcion"":""CAMPO DE MAYO"",
""CodigoPostal"":""1659""
},
{ 
""Descripcion"":""SAN MIGUEL"",
""CodigoPostal"":""1663""
},
{ 
""Descripcion"":""MUÑIZ"",
""CodigoPostal"":""1663""
}]");

            localidades.ForEach(l =>
            {
                l.accesoDatos = conexion;
                l.crear();
            });

            var perfil = new Perfil()
            {
                nombre = "ADMINISTRADOR CONTABLE"
            };

            perfil.accesoDatos = conexion;
            perfil.crear();

            adminGen.perfiles = new List<Perfil>()
            {
                perfil
            };

            adminGen.crearUsuarioPerfiles();
        }
    }
}

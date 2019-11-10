using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class Reserva
    {
        public int reserva_id { get; set; }
        public DateTime fecha { get; set; }
        public int cantidad_persona { get; set; }
        public int rut_cliente { get; set; }
        public string e_mail_cliente { get; set; }
        public string fono_cliente { get; set; }
        public string nombre { get; set; }
        public string hora_inicio { get; set; }
        public string comentarios { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class Proveedor
    {
        public int proveedor_id { get; set; }
        public string nombre { get; set; }
        public string fono { get; set; }
        public string contacto { get; set; }
        public string e_mail { get; set; }
        public string direccion { get; set; }
        public string comuna { get; set; }
    }
}

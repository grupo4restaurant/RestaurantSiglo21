using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class Boleta
    {
        public int boleta_id { get; set; }
        public int usuario_id { get; set; }
        public int orden_mesa_id { get; set; }
        public int fecha { get; set; }
        public int total_neto { get; set; }
        public int total_iva { get; set; }
        public int tipo_pago { get; set; }
    }
}

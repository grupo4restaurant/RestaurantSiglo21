using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class DetalleOrdenMesa
    {
        public int det_ord_mesa_id { get; set; }
        public int menu_id { get; set; }
        public int order_mesa_id { get; set; }
        public int cantidad { get; set; }
        public int estado { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class DetalleOrdenCompra
    {
        public int det_ord_compra_id { get; set; }
        public int producto_id { get; set; }
        public int orden_compra_id { get; set; }
        public int cantidad { get; set; }
        public int total_neto { get; set; }
        public int estado { get; set; }
    }
}

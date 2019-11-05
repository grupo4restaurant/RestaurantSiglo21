using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class DetalleOrdenInsumo
    {
        public int det_ord_insumo_id { get; set; }
        public int orden_insumo_id { get; set; }
        public int producto_id { get; set; }
        public int um_id { get; set; }
        public int cantidad { get; set; }
        public int estado { get; set; }
    }
}

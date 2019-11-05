using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class DetalleProductoReceta
    {
        public int det_pro_receta_id { get; set; }
        public int receta_id { get; set; }
        public int producto_id { get; set; }
        public int um_id { get; set; }
        public int cantidad { get; set; }
    }
}

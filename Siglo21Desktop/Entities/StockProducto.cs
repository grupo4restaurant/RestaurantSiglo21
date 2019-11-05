using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class StockProducto
    {
        public int stock_id { get; set; }
        public int producto_id { get; set; }
        public int cantidad { get; set; }
        public int minimo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class OrdenMesa
    {
        public int order_mesa_id { get; set; }
        public int mesa_id { get; set; }
        public int usuario_id { get; set; }
        public DateTime fecha { get; set; }
        public int total { get; set; }
        public int estado { get; set; }
    }
}

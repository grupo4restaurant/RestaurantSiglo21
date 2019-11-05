using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class Receta
    {
        public int receta_id { get; set; }
        public int item_id { get; set; }
        public int precedimiento { get; set; }
        public int nombre { get; set; }
        public int tiempo_prep { get; set; }
    }
}

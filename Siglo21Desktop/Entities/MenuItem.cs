using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class MenuItem
    {
        public int item_id { get; set; }
        public int cat_menu_id { get; set; }
        public string item_nombre { get; set; }
        public string item_desc { get; set; }
        public int item_val { get; set; }
    }
}

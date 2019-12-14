using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Model
{
    class ProductoModel
    {
        public int producto_id { get; set; }
        public int cat_prod_id { get; set; }
        public int proveedor_id { get; set; }
        public string nombre { get; set; }
        public string cod { get; set; }
        public int valor_neto { get; set; }
        public string nombre_catalogo { get; set; }
        public string nombre_proveedor { get; set; }

        public int cantidad { get; set; }
        public int stock { get; set; }

    }
}

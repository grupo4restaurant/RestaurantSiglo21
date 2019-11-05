using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class OrdenCompra
    {
        public int orden_compra_id { get; set; }
        public int total_val_neto { get; set; }
        public int total_val_iva { get; set; }
        public int fecha_creacion { get; set; }
        public int fecha_gestionada { get; set; }
        public int fecha_recepcion { get; set; }
        public int estado { get; set; }
    }
}

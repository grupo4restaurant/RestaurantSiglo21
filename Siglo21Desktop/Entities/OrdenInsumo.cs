using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class OrdenInsumo
    {
        public int orden_insumo_id { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_gestionada { get; set; }
        public DateTime fecha_recepcion { get; set; }
        public int estado { get; set; }
    }
}

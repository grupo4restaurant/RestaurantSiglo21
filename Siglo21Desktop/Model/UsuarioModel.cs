using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Model
{
    class UsuarioModel
    {
        public int usuario_id { get; set; }
        public int rol_id { get; set; }
        public string nombre { get; set; }
        public string ap_paterno { get; set; }
        public string ap_materno { get; set; }
        public string e_mail { get; set; }
        public string fono { get; set; }
        public string rol_desc { get; set; }
        public int rol_index { get; set; }
        
    }
}

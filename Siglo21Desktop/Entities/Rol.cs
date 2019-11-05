using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Entities
{
    class Rol
    {
        //[JsonProperty("rol_id")]
        public int rol_id { get; set; }
        //[JsonProperty("rol_desc")]
        public string rol_desc { get; set; }
        //[JsonProperty("rol_index")]
        public int rol_index { get; set; } 
    }
}

using Siglo21Desktop.Entities;
using Siglo21Desktop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Dao
{
    class RolDAO
    {
        HttpClient Client { get; set; }

        public RolDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(Rol obj) 
        {
            string ruta = CommonEnums.CrudPath.RolCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(Rol obj) 
        {
            string ruta = CommonEnums.CrudPath.RolCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);
            
            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            
            string ruta = CommonEnums.CrudPath.RolCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);                

            return response;
        }

        public async Task<Rol> GetById(int id) 
        {
            string ruta = CommonEnums.CrudPath.RolCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta); 

            if (response.IsSuccessStatusCode)
            {

                var item =  (await response.Content.ReadAsAsync<IEnumerable<Rol>>()).FirstOrDefault();
                return item;
            }

            return null;

        }
    }
}

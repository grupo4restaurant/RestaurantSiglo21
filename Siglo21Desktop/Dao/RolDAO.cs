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

        public async Task<int> Save(Rol obj) 
        {
            string ruta = CommonEnums.CrudPath.RolCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            if (response.IsSuccessStatusCode)
            {

                var item = await response.Content.ReadAsAsync<int>();
                return item;
            }

            return 0;
        }

        public async Task<Boolean> Update(Rol obj) 
        {
            string ruta = CommonEnums.CrudPath.RolCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            if (response.IsSuccessStatusCode)
            {

                var item = await response.Content.ReadAsAsync<Boolean>();
                return item;
            }
            return false;
        }

        public async Task<Boolean> Delete(int id)
        {
            
            string ruta = CommonEnums.CrudPath.RolCrud;
            var response = await Client.DeleteAsync(ruta + id);

            if (response.IsSuccessStatusCode)
            {

                var item = await response.Content.ReadAsAsync<Boolean>();
                return item;
            }
            return false;
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

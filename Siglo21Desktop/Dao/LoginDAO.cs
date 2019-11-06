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
    class LoginDAO
    {

        HttpClient Client { get; set; }

        public LoginDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(Login obj)
        {
            string ruta = CommonEnums.CrudPath.LoginCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(Login obj)
        {
            string ruta = CommonEnums.CrudPath.LoginCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.LoginCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<Login> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.LoginCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Login>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

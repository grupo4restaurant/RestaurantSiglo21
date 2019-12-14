using Siglo21Desktop.Entities;
using Siglo21Desktop.Enums;
using Siglo21Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Dao
{
    class MesaDAO
    {

        HttpClient Client { get; set; }

        public MesaDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(Mesa obj)
        {
            string ruta = CommonEnums.CrudPath.MesaCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(Mesa obj)
        {
            string ruta = CommonEnums.CrudPath.MesaCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.MesaCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<Mesa> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.MesaCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Mesa>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

        public async Task<List<Mesa>> GetAll()
        {
            string ruta = "http://localhost:8090/siglo21/mesa_todo/";

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Mesa>>()).ToList();
                return item;
            }

            return null;

        }
    }
}

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
    class OrdenMesaDAO
    {

        HttpClient Client { get; set; }

        public OrdenMesaDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(OrdenMesa obj)
        {
            string ruta = CommonEnums.CrudPath.OrdenMesaCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(OrdenMesa obj)
        {
            string ruta = CommonEnums.CrudPath.OrdenMesaCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.OrdenMesaCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<OrdenMesa> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.OrdenMesaCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<OrdenMesa>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

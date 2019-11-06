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
    class UnidadMedidaDAO
    {

        HttpClient Client { get; set; }

        public UnidadMedidaDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(UnidadMedida obj)
        {
            string ruta = CommonEnums.CrudPath.UnidadMedidaCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(UnidadMedida obj)
        {
            string ruta = CommonEnums.CrudPath.UnidadMedidaCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.UnidadMedidaCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<UnidadMedida> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.UnidadMedidaCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<UnidadMedida>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

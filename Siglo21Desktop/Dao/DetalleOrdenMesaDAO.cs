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

    class DetalleOrdenMesaDAO
    {

        HttpClient Client { get; set; }

        public DetalleOrdenMesaDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(DetalleOrdenMesa obj)
        {
            string ruta = CommonEnums.CrudPath.DetalleOrdenMesaCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(DetalleOrdenMesa obj)
        {
            string ruta = CommonEnums.CrudPath.DetalleOrdenMesaCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.DetalleOrdenMesaCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<DetalleOrdenMesa> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.DetalleOrdenMesaCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<DetalleOrdenMesa>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

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
    class DetalleProductoRecetaDAO
    {

        HttpClient Client { get; set; }

        public DetalleProductoRecetaDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(DetalleProductoReceta obj)
        {
            string ruta = CommonEnums.CrudPath.DetalleProductoRecetaCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(DetalleProductoReceta obj)
        {
            string ruta = CommonEnums.CrudPath.DetalleProductoRecetaCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.DetalleProductoRecetaCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<DetalleProductoReceta> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.DetalleProductoRecetaCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<DetalleProductoReceta>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

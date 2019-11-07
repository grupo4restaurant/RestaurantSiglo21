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
    class DetalleOrdenInsumoDAO
    {
        HttpClient Client { get; set; }


        public DetalleOrdenInsumoDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(DetalleOrdenInsumo obj)
        {
            string ruta = CommonEnums.CrudPath.DetalleOrdenInsumoCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(DetalleOrdenInsumo obj)
        {
            string ruta = CommonEnums.CrudPath.DetalleOrdenInsumoCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.DetalleOrdenInsumoCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<DetalleOrdenInsumo> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.DetalleOrdenInsumoCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<DetalleOrdenInsumo>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

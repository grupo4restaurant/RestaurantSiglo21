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
    class ProductoDAO
    {

        HttpClient Client { get; set; }

        public ProductoDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(Producto obj)
        {
            string ruta = CommonEnums.CrudPath.ProductoCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(Producto obj)
        {
            string ruta = CommonEnums.CrudPath.ProductoCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.ProductoCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<Producto> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.ProductoCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Producto>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

        public async Task<Producto> GetByCategoryId(int idCategoria)
        {
            string ruta = CommonEnums.ListadoPath.ProductosByCategoriaId + idCategoria;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Producto>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

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
    class OrdenCompraDAO
    {

        HttpClient Client { get; set; }

        public OrdenCompraDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(OrdenCompra obj)
        {
            string ruta = CommonEnums.CrudPath.OrdenCompraCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(OrdenCompra obj)
        {
            string ruta = CommonEnums.CrudPath.OrdenCompraCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.OrdenCompraCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<OrdenCompra> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.OrdenCompraCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<OrdenCompra>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

        public async Task<List<OrdenCompra>> GetAll()
        {
            string ruta = CommonEnums.ListadoPath.OrdenCompraTodo;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<OrdenCompra>>()).ToList();
                return item;
            }

            return null;

        }

    }
}

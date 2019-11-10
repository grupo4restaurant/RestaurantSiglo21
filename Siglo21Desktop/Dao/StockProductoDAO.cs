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
    class StockProductoDAO
    {

        HttpClient Client { get; set; }

        public StockProductoDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(StockProducto obj)
        {
            string ruta = CommonEnums.CrudPath.StockProductoCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(StockProducto obj)
        {
            string ruta = CommonEnums.CrudPath.StockProductoCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.StockProductoCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<StockProducto> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.StockProductoCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<StockProducto>>()).FirstOrDefault();
                return item;
            }

            return null;

        }
    }
}

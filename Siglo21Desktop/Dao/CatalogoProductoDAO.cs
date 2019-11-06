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
    class CatalogoProductoDAO
    {

        HttpClient Client { get; set; }

        public CatalogoProductoDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(CatalogoProducto obj)
        {
            string ruta = CommonEnums.CrudPath.CatalogoProductoCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(CatalogoProducto obj)
        {
            string ruta = CommonEnums.CrudPath.CatalogoProductoCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.CatalogoProductoCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<CatalogoProducto> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.CatalogoProductoCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<CatalogoProducto>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

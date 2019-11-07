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
    class OrdenInsumoDAO
    {

        HttpClient Client { get; set; }

        public OrdenInsumoDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(OrdenInsumo obj)
        {
            string ruta = CommonEnums.CrudPath.OrdenInsumoCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(OrdenInsumo obj)
        {
            string ruta = CommonEnums.CrudPath.OrdenInsumoCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.OrdenInsumoCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<OrdenInsumo> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.OrdenInsumoCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<OrdenInsumo>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

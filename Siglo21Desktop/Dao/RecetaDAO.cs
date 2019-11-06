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
    class RecetaDAO
    {

        HttpClient Client { get; set; }

        public RecetaDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(Receta obj)
        {
            string ruta = CommonEnums.CrudPath.RecetaCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(Receta obj)
        {
            string ruta = CommonEnums.CrudPath.RecetaCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.RecetaCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<Receta> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.RecetaCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Receta>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

    }
}

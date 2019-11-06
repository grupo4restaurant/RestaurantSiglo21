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
    class BoletaDAO
    {

        HttpClient Client { get; set; }

        public BoletaDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(Boleta obj)
        {
            string ruta = CommonEnums.CrudPath.BoletaCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(Boleta obj)
        {
            string ruta = CommonEnums.CrudPath.BoletaCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.BoletaCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<Boleta> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.BoletaCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Boleta>>()).FirstOrDefault();
                return item;
            }

            return null;

        }


    }
}

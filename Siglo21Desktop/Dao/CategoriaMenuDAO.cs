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
    class CategoriaMenuDAO
    {
        HttpClient Client { get; set; }

        public CategoriaMenuDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(CategoriaMenu obj)
        {
            string ruta = CommonEnums.CrudPath.CategoriaMenuCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(CategoriaMenu obj)
        {
            string ruta = CommonEnums.CrudPath.CategoriaMenuCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.CategoriaMenuCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<CategoriaMenu> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.CategoriaMenuCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<CategoriaMenu>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

        public async Task<List<CategoriaMenu>> GetAll()
        {
            string ruta = CommonEnums.ListadoPath.CategoriaMenus;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<CategoriaMenu>>()).ToList();
                return item;
            }

            return null;

        }


    }
}

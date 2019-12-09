using Siglo21Desktop.Entities;
using Siglo21Desktop.Enums;
using Siglo21Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.Dao
{
    class MenuItemDAO
    {

        HttpClient Client { get; set; }

        public MenuItemDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(MenuItem obj)
        {
            string ruta = CommonEnums.CrudPath.MenutemCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(MenuItem obj)
        {
            string ruta = CommonEnums.CrudPath.MenutemCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.MenutemCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<MenuItem> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.MenutemCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<MenuItem>>()).FirstOrDefault();
                return item;
            }

            return null;

        }
        //Obtener todo
        public async Task<List<MenuItem>> GetAll()
        {
            string ruta = CommonEnums.ListadoPath.MenuItems;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<MenuItem>>()).ToList();
                return item;
            }

            return null;

        }
        //obtener todo con cruce de categoria
        public async Task<List<MenuItemModel>> GetAllJoinCategoria()
        {
            string ruta = CommonEnums.ListadoPath.MenuItemsCategoria;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<MenuItemModel>>()).ToList();
                return item;
            }

            return null;

        }
    }
}

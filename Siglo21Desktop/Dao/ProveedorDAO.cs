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
    class ProveedorDAO
    {

        HttpClient Client { get; set; }

        public ProveedorDAO()
        {
            this.Client = new HttpClient();
        }

        public async Task<HttpResponseMessage> Save(Proveedor obj)
        {
            string ruta = CommonEnums.CrudPath.ProveedorCrud;
            var response = await Client.PutAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Update(Proveedor obj)
        {
            string ruta = CommonEnums.CrudPath.ProveedorCrud;
            var response = await Client.PostAsJsonAsync(ruta, obj);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {

            string ruta = CommonEnums.CrudPath.ProveedorCrud;
            HttpResponseMessage response = await Client.DeleteAsync(ruta + id);

            return response;
        }

        public async Task<Proveedor> GetById(int id)
        {
            string ruta = CommonEnums.CrudPath.ProveedorCrud + id;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Proveedor>>()).FirstOrDefault();
                return item;
            }

            return null;

        }

        public async Task<List<Proveedor>> GetAll()
        {
            string ruta = CommonEnums.ListadoPath.Proveedores;

            HttpResponseMessage response = await Client.GetAsync(ruta);

            if (response.IsSuccessStatusCode)
            {

                var item = (await response.Content.ReadAsAsync<IEnumerable<Proveedor>>()).ToList();
                return item;
            }

            return null;

        }

    }
}

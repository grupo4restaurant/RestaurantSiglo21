using Caliburn.Micro;
using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
using Siglo21Desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Siglo21Desktop
{
    public static class DataAccess
    {
        
        internal static ObservableCollection<MenuItemModel> GetMenuItems(int start, int itemCount, string sortColumn, bool ascending, out int totalItems, BindableCollection<MenuItemModel> lista)
        {
            totalItems = lista.Count;

            ObservableCollection<MenuItemModel> sorted = new ObservableCollection<MenuItemModel>();

            // Sort the products. In reality, the items should be stored in a database and
            // use SQL statements for sorting and querying items.
            switch (sortColumn)
            {
                case ("Id"):
                    sorted = new ObservableCollection<MenuItemModel>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.item_id
                        select p
                    );
                    break;
                case ("Name"):
                    sorted = new ObservableCollection<MenuItemModel>
                    (
                        from p in lista
                        orderby p.item_nombre
                        select p
                    );
                    break;
                case ("Categoria"):
                    sorted = new ObservableCollection<MenuItemModel>
                    (
                        from p in lista
                        orderby p.cat_menu_nombre
                        select p
                    );
                    break;
            }

            sorted = ascending ? sorted : new ObservableCollection<MenuItemModel>(sorted.Reverse());

            ObservableCollection<MenuItemModel> filtered = new ObservableCollection<MenuItemModel>();

            for (int i = start; i < start + itemCount && i < totalItems; i++)
            {
                filtered.Add(sorted[i]);
            }

            return filtered;
        }

        internal static ObservableCollection<MesaModel> GetMesa(int start, int itemCount, string sortColumn, bool ascending, out int totalItems, BindableCollection<MesaModel> lista)
        {
            totalItems = lista.Count;

            ObservableCollection<MesaModel> sorted = new ObservableCollection<MesaModel>();

            // Sort the products. In reality, the items should be stored in a database and
            // use SQL statements for sorting and querying items.
            switch (sortColumn)
            {
                case ("Tipo1"):
                    sorted = new ObservableCollection<MesaModel>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.mesa_id
                        select p
                    );
                    break;
                case ("Tipo2"):
                    sorted = new ObservableCollection<MesaModel>
                    (
                        from p in lista
                        orderby p.mesa_numero
                        select p
                    );
                    break;
                case ("Tipo3"):
                    sorted = new ObservableCollection<MesaModel>
                    (
                        from p in lista
                        orderby p.mesa_numero_descripcion
                        select p
                    );
                    break;
                case ("Tipo4"):
                    sorted = new ObservableCollection<MesaModel>
                    (
                        from p in lista
                        orderby p.mesa_capacidad
                        select p
                    );
                    break;
            }

            sorted = ascending ? sorted : new ObservableCollection<MesaModel>(sorted.Reverse());

            ObservableCollection<MesaModel> filtered = new ObservableCollection<MesaModel>();

            for (int i = start; i < start + itemCount && i < totalItems; i++)
            {
                filtered.Add(sorted[i]);
            }

            return filtered;
        }

        internal static ObservableCollection<Proveedor> GetProveedores(int start, int itemCount, string sortColumn, bool ascending, out int totalItems, BindableCollection<Proveedor> lista)
        {
            totalItems = lista.Count;

            ObservableCollection<Proveedor> sorted = new ObservableCollection<Proveedor>();

            // Sort the products. In reality, the items should be stored in a database and
            // use SQL statements for sorting and querying items.
            switch (sortColumn)
            {
                case ("Tipo1"):
                    sorted = new ObservableCollection<Proveedor>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.nombre
                        select p
                    );
                    break;
                case ("Tipo2"):
                    sorted = new ObservableCollection<Proveedor>
                    (
                        from p in lista
                        orderby p.fono
                        select p
                    );
                    break;
                case ("Tipo3"):
                    sorted = new ObservableCollection<Proveedor>
                    (
                        from p in lista
                        orderby p.contacto
                        select p
                    );
                    break;
                case ("Tipo4"):
                    sorted = new ObservableCollection<Proveedor>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.e_mail
                        select p
                    );
                    break;
                case ("Tipo5"):
                    sorted = new ObservableCollection<Proveedor>
                    (
                        from p in lista
                        orderby p.direccion
                        select p
                    );
                    break;
                //case ("Tipo6"):
                //    sorted = new ObservableCollection<Proveedor>
                //    (
                //        from p in lista
                //        orderby p.comuna
                //        select p
                //    );
                //    break;
            }

            sorted = ascending ? sorted : new ObservableCollection<Proveedor>(sorted.Reverse());

            ObservableCollection<Proveedor> filtered = new ObservableCollection<Proveedor>();

            for (int i = start; i < start + itemCount && i < totalItems; i++)
            {
                filtered.Add(sorted[i]);
            }

            return filtered;
        }

        internal static ObservableCollection<UsuarioModel> GetUsuarios(int start, int itemCount, string sortColumn, bool ascending, out int totalItems, BindableCollection<UsuarioModel> lista)
        {
            totalItems = lista.Count;

            ObservableCollection<UsuarioModel> sorted = new ObservableCollection<UsuarioModel>();

            // Sort the products. In reality, the items should be stored in a database and
            // use SQL statements for sorting and querying items.
            switch (sortColumn)
            {
                case ("Tipo1"):
                    sorted = new ObservableCollection<UsuarioModel>
                    (
                        from p in lista
                        orderby p.usuario_id
                        select p
                    );
                    break;
                case ("Tipo2"):
                    sorted = new ObservableCollection<UsuarioModel>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.nombre
                        select p
                    );
                    break;
                case ("Tipo3"):
                    sorted = new ObservableCollection<UsuarioModel>
                    (
                        from p in lista
                        orderby p.ap_paterno
                        select p
                    );
                    break;
                case ("Tipo4"):
                    sorted = new ObservableCollection<UsuarioModel>
                    (
                        from p in lista
                        orderby p.ap_materno
                        select p
                    );
                    break;
                case ("Tipo5"):
                    sorted = new ObservableCollection<UsuarioModel>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.e_mail
                        select p
                    );
                    break;
                case ("Tipo6"):
                    sorted = new ObservableCollection<UsuarioModel>
                    (
                        from p in lista
                        orderby p.fono
                        select p
                    );
                    break;
                case ("Tipo7"):
                    sorted = new ObservableCollection<UsuarioModel>
                    (
                        from p in lista
                        orderby p.rol_desc
                        select p
                    );
                    break;
            }

            sorted = ascending ? sorted : new ObservableCollection<UsuarioModel>(sorted.Reverse());

            ObservableCollection<UsuarioModel> filtered = new ObservableCollection<UsuarioModel>();

            for (int i = start; i < start + itemCount && i < totalItems; i++)
            {
                filtered.Add(sorted[i]);
            }

            return filtered;
        }

        internal static ObservableCollection<ProductoModel> GetProductos(int start, int itemCount, string sortColumn, bool ascending, out int totalItems, BindableCollection<ProductoModel> lista)
        {
            totalItems = lista.Count;

            ObservableCollection<ProductoModel> sorted = new ObservableCollection<ProductoModel>();

            // Sort the products. In reality, the items should be stored in a database and
            // use SQL statements for sorting and querying items.
            switch (sortColumn)
            {
                case ("Tipo1"):
                    sorted = new ObservableCollection<ProductoModel>
                    (
                        from p in lista
                        orderby p.producto_id
                        select p
                    );
                    break;
                case ("Tipo2"):
                    sorted = new ObservableCollection<ProductoModel>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.nombre
                        select p
                    );
                    break;
                case ("Tipo3"):
                    sorted = new ObservableCollection<ProductoModel>
                    (
                        from p in lista
                        orderby p.cod
                        select p
                    );
                    break;
                case ("Tipo4"):
                    sorted = new ObservableCollection<ProductoModel>
                    (
                        from p in lista
                        orderby p.nombre_catalogo
                        select p
                    );
                    break;
                case ("Tipo5"):
                    sorted = new ObservableCollection<ProductoModel>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.nombre_proveedor
                        select p
                    );
                    break;
                case ("Tipo6"):
                    sorted = new ObservableCollection<ProductoModel>
                    (
                        from p in lista
                        orderby p.valor_neto
                        select p
                    );
                    break;
                case ("Tipo7"):
                    sorted = new ObservableCollection<ProductoModel>
                    (
                        from p in lista
                        orderby p.stock
                        select p
                    );
                    break;

            }

            sorted = ascending ? sorted : new ObservableCollection<ProductoModel>(sorted.Reverse());

            ObservableCollection<ProductoModel> filtered = new ObservableCollection<ProductoModel>();

            for (int i = start; i < start + itemCount && i < totalItems; i++)
            {
                filtered.Add(sorted[i]);
            }

            return filtered;
        }

        internal static ObservableCollection<OrdenCompra> GetOrdenCompras(int start, int itemCount, string sortColumn, bool ascending, out int totalItems, BindableCollection<OrdenCompra> lista)
        {
            totalItems = lista.Count;

            ObservableCollection<OrdenCompra> sorted = new ObservableCollection<OrdenCompra>();

            // Sort the products. In reality, the items should be stored in a database and
            // use SQL statements for sorting and querying items.
            switch (sortColumn)
            {
                case ("Tipo1"):
                    sorted = new ObservableCollection<OrdenCompra>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.fecha_creacion
                        select p
                    );
                    break;
                case ("Tipo2"):
                    sorted = new ObservableCollection<OrdenCompra>
                    (
                        from p in lista
                        orderby p.fecha_gestionada
                        select p
                    );
                    break;
                case ("Tipo3"):
                    sorted = new ObservableCollection<OrdenCompra>
                    (
                        from p in lista
                        orderby p.fecha_recepcion
                        select p
                    );
                    break;
                case ("Tipo4"):
                    sorted = new ObservableCollection<OrdenCompra>
                    (
                        //obtiene el listado de datos de la entidad representado por products
                        from p in lista
                        orderby p.estado
                        select p
                    );
                    break;
                case ("Tipo5"):
                    sorted = new ObservableCollection<OrdenCompra>
                    (
                        from p in lista
                        orderby p.total_val_neto
                        select p
                    );
                    break;
                case ("Tipo6"):
                    sorted = new ObservableCollection<OrdenCompra>
                    (
                        from p in lista
                        orderby p.total_val_iva
                        select p
                    );
                    break;
            }

            sorted = ascending ? sorted : new ObservableCollection<OrdenCompra>(sorted.Reverse());

            ObservableCollection<OrdenCompra> filtered = new ObservableCollection<OrdenCompra>();

            for (int i = start; i < start + itemCount && i < totalItems; i++)
            {
                filtered.Add(sorted[i]);
            }

            return filtered;
        }

    }
}

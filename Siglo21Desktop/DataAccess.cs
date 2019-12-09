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

        
    }
}

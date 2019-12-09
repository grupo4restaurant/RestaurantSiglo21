using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
using Siglo21Desktop.Formulario.Recursos.MenuForm;
using Siglo21Desktop.Helpers;
using Siglo21Desktop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Siglo21Desktop.Control.Recursos
{
    /// <summary>
    /// Interaction logic for RecursosMenuUC.xaml
    /// </summary>
    public partial class RecursosMenuUC : UserControl
    {
        private DataGridColumn currentSortColumn;

        private ListSortDirection currentSortDirection;

        public RecursosMenuUC()
        {
            InitializeComponent();
            
            DataContext = new PaginacionMenu();
            dg.Columns[0].Header = "Id";
            dg.Columns[1].Header = "Nombre";
            dg.Columns[2].Header = "Categoría";
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dg.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    MenuItemModel p = o as MenuItemModel;
                                
                    if (t.Name == "txtId" && IsNumeric(filter))
                    {
                        return (p.item_id == Convert.ToInt32(filter));
                    }
                    if (t.Name == "txtCategoria")
                    {
                        return (p.cat_menu_nombre.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    return (p.item_nombre.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }
        


        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MenuItemModel dataRowView = (MenuItemModel)((Button)e.Source).DataContext;
            int item_id = dataRowView.item_id;
            
            ActualizarMenu ventana = new ActualizarMenu(item_id);
            App.Current.MainWindow = ventana;
            ventana.Show();

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MenuItemModel dataRowView = (MenuItemModel)((Button)e.Source).DataContext;
            int item_id = dataRowView.item_id;
            MenuItemDAO dao = new MenuItemDAO();
            try
            {
                var response = await dao.Delete(item_id);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Item Menú Exitosamente Borrado!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Item Menú no Borrado!");
            }
        }
        
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            // The current sorted column must be specified in XAML.
            currentSortColumn = dataGrid.Columns.Where(c => c.SortDirection.HasValue).Single();
            currentSortDirection = currentSortColumn.SortDirection.Value;
        }

        
        private void DataGrid_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (currentSortColumn != null)
            {
                currentSortColumn.SortDirection = currentSortDirection;
            }
        }

        
        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true;

            PaginacionMenu paginacionMenu = (PaginacionMenu)DataContext;

            string sortField = String.Empty;

            // Use a switch statement to check the SortMemberPath
            // and set the sort column to the actual column name. In this case,
            // the SortMemberPath and column names match.
            switch (e.Column.SortMemberPath)
            {
                case ("item_id"):
                    sortField = "Id";
                    break;
                case ("item_nombre"):
                    sortField = "Name";
                    break;
                case ("cat_menu_nombre"):
                    sortField = "Categoria";
                    break;
            }

            ListSortDirection direction = (e.Column.SortDirection != ListSortDirection.Ascending) ?
                ListSortDirection.Ascending : ListSortDirection.Descending;

            bool sortAscending = direction == ListSortDirection.Ascending;

            paginacionMenu.Sort(sortField, sortAscending);

            currentSortColumn.SortDirection = null;

            e.Column.SortDirection = direction;

            currentSortColumn = e.Column;
            currentSortDirection = direction;
        }

        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            IngresoMenu ventana = new IngresoMenu();
            App.Current.MainWindow = ventana;
            ventana.Show();
        }
    }
}



    

    

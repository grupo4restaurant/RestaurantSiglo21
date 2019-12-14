using Siglo21Desktop.Dao;
using Siglo21Desktop.Formulario.Recursos.ProductoForm;
using Siglo21Desktop.Helpers;
using Siglo21Desktop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for RecursosProductoUC.xaml
    /// </summary>
    public partial class RecursosProductoUC : UserControl
    {
        private DataGridColumn currentSortColumn;

        private ListSortDirection currentSortDirection;

        public RecursosProductoUC()
        {
            InitializeComponent();

            DataContext = new PaginacionProducto();
            dg.Columns[0].Visibility = Visibility.Hidden;
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
                    ProductoModel p = o as ProductoModel;

                    if (t.Name == "txtId" && IsNumeric(filter))
                    {
                        return (p.producto_id == Convert.ToInt32(filter));
                    }
                    if (t.Name == "txtNombre")
                    {
                        return (p.nombre.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtCod")
                    {
                        return (p.cod.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtCategoria")
                    {
                        return (p.nombre_catalogo.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtProveedor")
                    {
                        return (p.nombre_proveedor.ToUpper().StartsWith(filter.ToUpper()));
                    }
                   
                    return (p.valor_neto == Convert.ToInt32(filter));
                    
                };
            }
        }



        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ProductoModel dataRowView = (ProductoModel)((Button)e.Source).DataContext;
            int producto_id = dataRowView.producto_id;

            ActualizarProducto ventana = new ActualizarProducto(producto_id);
            App.Current.MainWindow = ventana;
            ventana.Show();

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ProductoModel dataRowView = (ProductoModel)((Button)e.Source).DataContext;
            int producto_id = dataRowView.producto_id;
            ProductoDAO dao = new ProductoDAO();

            try
            {
                //borrar                
                var response = await dao.Delete(producto_id);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Producto Exitosamente Borrado!");
                    DataContext = new PaginacionProducto();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Producto no Borrado!");
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            // The current sorted column must be specified in XAML.
            currentSortColumn = dataGrid.Columns.Where(c => c.SortDirection.HasValue).FirstOrDefault();
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

            PaginacionProducto paginacionMenu = (PaginacionProducto)DataContext;

            string sortField = String.Empty;

            // Use a switch statement to check the SortMemberPath
            // and set the sort column to the actual column name. In this case,
            // the SortMemberPath and column names match.
            switch (e.Column.SortMemberPath)
            {
                case ("producto_id"):
                    sortField = "Tipo1";
                    break;
                case ("nombre"):
                    sortField = "Tipo2";
                    break;
                case ("cod"):
                    sortField = "Tipo3";
                    break;
                case ("nombre_catalogo"):
                    sortField = "Tipo4";
                    break;
                case ("nombre_proveedor"):
                    sortField = "Tipo5";
                    break;
                case ("valor_neto"):
                    sortField = "Tipo6";
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
            IngresoProducto ventana = new IngresoProducto();
            App.Current.MainWindow = ventana;
            ventana.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PaginacionProducto();
        }
    }
}

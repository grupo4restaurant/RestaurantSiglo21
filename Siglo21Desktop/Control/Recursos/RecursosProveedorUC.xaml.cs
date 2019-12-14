using System;
using System.Collections.Generic;
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
using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
using System.Data;
using System.ComponentModel;
using Siglo21Desktop.Helpers;
using Siglo21Desktop.Formulario.Recursos.ProveedorForm;

namespace Siglo21Desktop.Control.Recursos
{
    /// <summary>
    /// Interaction logic for RecursosProveedorUC.xaml
    /// </summary>
    public partial class RecursosProveedorUC : UserControl
    {
        private DataGridColumn currentSortColumn;

        private ListSortDirection currentSortDirection;

        public RecursosProveedorUC()
        {
            InitializeComponent();

            DataContext = new PaginacionProveedor();

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
                    Proveedor p = o as Proveedor;

                    if (t.Name == "txtId" && IsNumeric(filter))
                    {
                        return (p.proveedor_id == Convert.ToInt32(filter));
                    }
                    if (t.Name == "txtNombre")
                    {
                        return (p.nombre.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtFono")
                    {
                        return (p.fono.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtContacto")
                    {
                        return (p.contacto.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtEmail")
                    {
                        return (p.e_mail.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtDireccion")
                    {
                        return (p.direccion.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    return (p.comuna.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }



        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Proveedor dataRowView = (Proveedor)((Button)e.Source).DataContext;
            int proveedor_id = dataRowView.proveedor_id;

            ActualizarProveedor ventana = new ActualizarProveedor(proveedor_id);
            App.Current.MainWindow = ventana;
            ventana.Show();

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Proveedor dataRowView = (Proveedor)((Button)e.Source).DataContext;
            int proveedor_id = dataRowView.proveedor_id;
            ProveedorDAO dao = new ProveedorDAO();
            try
            {
                var response = await dao.Delete(proveedor_id);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Proveedor Exitosamente Borrado!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Proveedor no Borrado!");
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

            PaginacionProveedor paginacionMenu = (PaginacionProveedor)DataContext;

            string sortField = String.Empty;

            // Use a switch statement to check the SortMemberPath
            // and set the sort column to the actual column name. In this case,
            // the SortMemberPath and column names match.
            switch (e.Column.SortMemberPath)
            {
                case ("nombre"):
                    sortField = "Tipo1";
                    break;
                case ("fono"):
                    sortField = "Tipo2";
                    break;
                case ("contacto"):
                    sortField = "Tipo3";
                    break;
                case ("e_mail"):
                    sortField = "Tipo4";
                    break;
                case ("direccion"):
                    sortField = "Tipo5";
                    break;
                case ("comuna"):
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
            IngresoDeProveedores ventana = new IngresoDeProveedores();
            App.Current.MainWindow = ventana;
            ventana.Show();
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PaginacionProveedor();
        }
    }
}
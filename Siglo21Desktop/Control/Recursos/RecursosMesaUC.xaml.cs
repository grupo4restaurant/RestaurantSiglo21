using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
using Siglo21Desktop.Formulario.Recursos.MesaForm;
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
    /// Interaction logic for RecursosMesaUC.xaml
    /// </summary>
    public partial class RecursosMesaUC : UserControl
    {
        private DataGridColumn currentSortColumn;

        private ListSortDirection currentSortDirection;

        public RecursosMesaUC()
        {
            InitializeComponent();

            DataContext = new PaginacionMesa();
            //esconde columna del ID
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
                    MesaModel p = o as MesaModel;

                    if (t.Name == "txtId" && IsNumeric(filter))
                    {
                        return (p.mesa_id == Convert.ToInt32(filter));
                    }
                    if (t.Name == "txtCapacidad" && IsNumeric(filter))
                    {
                        return (p.mesa_capacidad == Convert.ToInt32(filter));
                    }
                    if (t.Name == "txtDescripcion")
                    {
                        return (p.mesa_numero_descripcion.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    return (p.mesa_numero.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }



        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MesaModel dataRowView = (MesaModel)((Button)e.Source).DataContext;
            int mesa_id = dataRowView.mesa_id;

            ActualizarMesa ventana = new ActualizarMesa(mesa_id);
            App.Current.MainWindow = ventana;
            ventana.Show();

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MesaModel dataRowView = (MesaModel)((Button)e.Source).DataContext;
            int mesa_id = dataRowView.mesa_id;
            MesaDAO dao = new MesaDAO();
            try
            {
                var response = await dao.Delete(mesa_id);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Mesa Exitosamente Borrado!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Mesa no Borrado!");
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

            PaginacionMesa paginacionMenu = (PaginacionMesa)DataContext;

            string sortField = String.Empty;

            // Use a switch statement to check the SortMemberPath
            // and set the sort column to the actual column name. In this case,
            // the SortMemberPath and column names match.
            switch (e.Column.SortMemberPath)
            {
                case ("mesa_id"):
                    sortField = "Tipo1";
                    break;
                case ("mesa_numero"):
                    sortField = "Tipo2";
                    break;
                case ("mesa_capacidad"):
                    sortField = "Tipo3";
                    break;
                case ("mesa_numero_descripcion"):
                    sortField = "Tipo4";
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
            IngresoMesa ventana = new IngresoMesa();
            App.Current.MainWindow = ventana;
            ventana.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PaginacionMesa();
        }
    }
}


using Newtonsoft.Json;
using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
using Siglo21Desktop.Formulario.Recursos.UsuarioForm;
using Siglo21Desktop.Helpers;
using Siglo21Desktop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for RecursosUsuarioUC.xaml
    /// </summary>
    public partial class RecursosUsuarioUC : UserControl
    {
        private DataGridColumn currentSortColumn;

        private ListSortDirection currentSortDirection;

        public RecursosUsuarioUC()
        {
            InitializeComponent();

            DataContext = new PaginacionUsuario();
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
                    UsuarioModel p = o as UsuarioModel;

                    if (t.Name == "txtId" && IsNumeric(filter))
                    {
                        return (p.usuario_id == Convert.ToInt32(filter));
                    }
                    if (t.Name == "txtNombre")
                    {
                        return (p.nombre.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtPaterno")
                    {
                        return (p.ap_paterno.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtMaterno")
                    {
                        return (p.ap_materno.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtEmail")
                    {
                        return (p.e_mail.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    if (t.Name == "txtFono")
                    {
                        return (p.fono.ToUpper().StartsWith(filter.ToUpper()));
                    }
                    return (p.rol_desc.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }



        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UsuarioModel dataRowView = (UsuarioModel)((Button)e.Source).DataContext;
            int usuario_id = dataRowView.usuario_id;

            ActualizarUsuario ventana = new ActualizarUsuario(usuario_id);
            App.Current.MainWindow = ventana;
            ventana.Show();

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            UsuarioModel dataRowView = (UsuarioModel)((Button)e.Source).DataContext;
            int usuario_id = dataRowView.usuario_id;
            UsuarioDAO dao = new UsuarioDAO();
            //LoginDAO loginDao = new LoginDAO();
            try
            {
                ////borrar login por uasuario_id
                //var login = await loginDao.Delete(usuario_id);
                //borrar usuario
                var response = await dao.Delete(usuario_id);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Usuario Exitosamente Borrado!");
                    DataContext = new PaginacionUsuario();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Usuario no Borrado!");
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

            PaginacionUsuario paginacionMenu = (PaginacionUsuario)DataContext;

            string sortField = String.Empty;

            // Use a switch statement to check the SortMemberPath
            // and set the sort column to the actual column name. In this case,
            // the SortMemberPath and column names match.
            switch (e.Column.SortMemberPath)
            {
                case ("usuario_id"):
                    sortField = "Tipo1";
                    break;
                case ("nombre"):
                    sortField = "Tipo2";
                    break;
                case ("ap_paterno"):
                    sortField = "Tipo3";
                    break;
                case ("ap_materno"):
                    sortField = "Tipo4";
                    break;
                case ("e_mail"):
                    sortField = "Tipo5";
                    break;
                case ("fono"):
                    sortField = "Tipo6";
                    break;
                case ("rol_desc"):
                    sortField = "Tipo7";
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
            IngresoUsuario ventana = new IngresoUsuario();
            App.Current.MainWindow = ventana;
            ventana.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PaginacionUsuario();
        }
    }
}

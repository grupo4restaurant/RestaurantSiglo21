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


namespace Siglo21Desktop.Control.Recursos
{
    /// <summary>
    /// Interaction logic for RecursosProveedorUC.xaml
    /// </summary>
    public partial class RecursosProveedorUC : UserControl
    {
        public RecursosProveedorUC()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProveedorDAO proveedorDao = new ProveedorDAO();
            try
            {
                dataGridProveedores.DataContext =  await proveedorDao.GetAll();
       
          
                //var result = await proveedorDao.GetAll();

            }
            catch (Exception)
            {
                MessageBox.Show("Proveedor no Encontrado");
            }

        }


        private void button_Click(object sender, RoutedEventArgs e)
        {

            IngresoProveedores main = new IngresoProveedores();
            App.Current.MainWindow = main;
            main.Show();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            //    DataTableCollection collection = ds.Tables;
            //    DataTable table = collection[0];




            //    foreach (DataRow row in table.Rows)
            //    {
            //        var data = new Proveedor { proveedor_id = row["Id"].ToString(), nombre = row["Nombre"].ToString(), fono = row["Fono"].ToString(), contacto = row["Contacto"].ToString(), e_mail = row["Email"].ToString(), direccion = row["Direccion"].ToString(), comuna = row["Comuna"].ToString() };
            //        dataGridProveedores.Items.Add(data);
            //    }


            ProveedorDAO proveedorDao = new ProveedorDAO();
            try
            {
                var result = await proveedorDao.GetById(1);

            }
            catch (Exception)
            {
                MessageBox.Show("Proveedor no Encontrado");
            }

        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {

            //ActualizarEliminarProveedor main = new ActualizarEliminarProveedor();
            //App.Current.MainWindow = main;
            //main.Show();
        }

        private void btnVer_Click(object sender, RoutedEventArgs e)
        {

            //ActualizarEliminarProveedor main = new ActualizarEliminarProveedor();
            //App.Current.MainWindow = main;
            //main.Show();
        }


    }
}

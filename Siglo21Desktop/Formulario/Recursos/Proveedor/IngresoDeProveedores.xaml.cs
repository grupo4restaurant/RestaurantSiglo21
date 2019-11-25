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
using System.Windows.Shapes;

using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;

namespace Siglo21Desktop.Formulario.Recursos.Proveedor
{
    /// <summary>
    /// Lógica de interacción para IngresoDeProveedores.xaml
    /// </summary>
    public partial class IngresoDeProveedores : Window
    {
        public IngresoDeProveedores()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            var textoNombre = txtNombre.Text;
            var textoFono = txtFono.Text;
            var textoContacto = txtContacto.Text;
            var textoEmail = txtEmail.Text;
            var textoDireccion = txtDireccion.Text;
            var textoComuna = txtComuna.Text;

            //ProveedorDAO proveedorDao = new ProveedorDAO();
            //try
            //{
            //    //Proveedor obj = new Proveedor()
            //    {

            //        //nombre = textoNombre,
            //        //fono = textoFono,
            //        //contacto = textoContacto,
            //        //e_mail = textoEmail,
            //        //direccion = textoDireccion,
            //        //comuna = textoComuna
            //    };
            //    var response = await proveedorDao.Save(obj);

            //    MessageBox.Show("Proveedor Añadido Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("proveedor no Añadido");
            //}
        }
    }
}

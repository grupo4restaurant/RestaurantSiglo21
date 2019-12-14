using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
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

namespace Siglo21Desktop.Formulario.Recursos.ProveedorForm
{
    /// <summary>
    /// Interaction logic for IngresoDeProveedores.xaml
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

            ProveedorDAO dao = new ProveedorDAO();
            var listadoProveedor = await dao.GetAll();
            var result = (from u in listadoProveedor
                          where u.nombre == textoNombre
                            
                          select new
                          {
                              u.proveedor_id
                          }).FirstOrDefault();

            if (result != null)
            {

                MessageBox.Show("Proveedor ya Existe");
                this.Close();

            }

            else




                try
            {
                Proveedor obj = new Proveedor()
                {

                    nombre = textoNombre,
                    fono = textoFono,
                    contacto = textoContacto,
                    e_mail = textoEmail,
                    direccion = textoDireccion,
                    comuna = textoComuna
                };
                var response = dao.Save(obj);

                MessageBox.Show("Proveedor Añadido Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("proveedor no Añadido");
            }
        }
    }
}

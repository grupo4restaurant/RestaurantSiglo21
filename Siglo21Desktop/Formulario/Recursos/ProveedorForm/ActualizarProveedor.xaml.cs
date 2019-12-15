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
    /// Interaction logic for ActualizarProveedor.xaml
    /// </summary>
    public partial class ActualizarProveedor : Window
    {
        private int proveedor_id;
        public ActualizarProveedor(int proveedor_id) 
        {
            this.proveedor_id = proveedor_id;
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }


        private async void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            int id = this.proveedor_id;
            string nombre = txtNombre.Text;
            string fono = txtFono.Text;
            string contacto = txtContacto.Text;
            string e_mail = txtEmail.Text;
            string direccion = txtDireccion.Text;
            string comuna = txtComuna.Text;


            ProveedorDAO dao = new ProveedorDAO();
            //var listadoProveedor = await dao.GetAll();
            //var result = (from u in listadoProveedor
            //              where u.nombre == nombre

            //              select new
            //              {
            //                  u.proveedor_id
            //              }).FirstOrDefault();

            //if (result != null)
            //{

            //    MessageBox.Show("Proveedor ya Existe");
            //    this.Close();

            //}

            //else


                try
            {
                Proveedor obj = new Proveedor()
                {
                    proveedor_id = id,
                    nombre = nombre,
                    fono = fono,
                    contacto = contacto,
                    e_mail = e_mail,
                    direccion = direccion,
                    comuna = comuna
                };
                var response = await dao.Update(obj);

                MessageBox.Show("Proveedor Actualizado Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Proveedor no Actualizado");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProveedorDAO proveedorDao = new ProveedorDAO();
            try
            {
                var proveedor = await proveedorDao.GetById(proveedor_id);

                txtNombre.Text = proveedor.nombre;
                txtFono.Text = proveedor.fono;
                txtContacto.Text = proveedor.contacto;
                txtEmail.Text = proveedor.e_mail;
                txtDireccion.Text = proveedor.direccion;
                txtComuna.Text = proveedor.comuna;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar datos en Actualizar");
            }
        }
    }
}
using Caliburn.Micro;
using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
using Siglo21Desktop.Helpers;
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

namespace Siglo21Desktop.Formulario.Recursos.UsuarioForm
{
    /// <summary>
    /// Interaction logic for IngresoUsuario.xaml
    /// </summary>
    public partial class IngresoUsuario : Window
    {
        public IngresoUsuario()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Rol selectedRol = this.PerfilCB.SelectedItem as Rol;
            int rol_id = selectedRol.rol_id;
            string nombre = txtNombre.Text;
            string ap_paterno = txtPaterno.Text;
            string ap_materno = txtMaterno.Text;

            string fono = txtFono.Text;

            UsuarioDAO dao = new UsuarioDAO();
            var listadoUsuario = await dao.GetAll();
            var result = (from u in listadoUsuario
                          where u.nombre == nombre
                             && u.ap_paterno == ap_paterno
                             && u.ap_materno == ap_materno
                          select new
                          {
                              u.usuario_id
                          }).FirstOrDefault();

            if (result != null) {

                MessageBox.Show("Usuario ya Existe");
                this.Close();

            }
           
            else 

            

            
            try
            {
                Usuario obj = new Usuario()
                {
                    rol_id = rol_id,
                    nombre = nombre,
                    ap_paterno = ap_paterno,
                    ap_materno = ap_materno,
                    e_mail = "aaa",
                    fono = fono
                };
                var response = await dao.Save(obj);



                MessageBox.Show("Usuario Añadido Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario no Añadido, Podría Existir Duplicidad de ID");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RolDAO rolDao = new RolDAO();
            try
            {
                var result = await rolDao.GetAll();
                BindableCollection<Rol> lista = new BindableCollection<Rol>(result);

                //opcion por defecto combobox
                Rol defaultCB = new Rol
                {
                    rol_id = 0,
                    rol_desc = "Seleccionar",
                    rol_index = 0
                };
                //insertar en la primera posición
                lista.Insert(0, defaultCB);

                PerfilCB.ItemsSource = lista;
                PerfilCB.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar listado Rol");
            }
        }
    }
}


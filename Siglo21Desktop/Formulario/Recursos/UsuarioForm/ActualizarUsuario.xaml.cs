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
    /// Interaction logic for ActualizarUsuario.xaml
    /// </summary>
    public partial class ActualizarUsuario : Window
    {
        private int usuario_id;
        public ActualizarUsuario(int usuario_id)
        {
            this.usuario_id = usuario_id;
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }


        private async void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Rol selectedRol = this.PerfilCB.SelectedItem as Rol;
            int rol_id = selectedRol.rol_id;
            string nombre = txtNombre.Text;
            string ap_paterno = txtPaterno.Text;
            string ap_materno = txtMaterno.Text;
            string e_mail = txtEmail.Text;
            string fono = txtFono.Text;


            UsuarioDAO dao = new UsuarioDAO();
            
            try
            { 

                Usuario obj = new Usuario()
                {
                    usuario_id = this.usuario_id,
                    rol_id = rol_id,
                    nombre = nombre,
                    ap_paterno = ap_paterno,
                    ap_materno = ap_materno,
                    e_mail = e_mail,
                    fono = fono
                };
            var response = await dao.Update(obj);

            MessageBox.Show("Usuario Actualizado Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuario no Actualizado");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RolDAO rolDao = new RolDAO();
            UsuarioDAO usuarioDao = new UsuarioDAO();
            try
            {
                //datos combobox
                var combobox = await rolDao.GetAll();
                BindableCollection<Rol> listaCB = new BindableCollection<Rol>(combobox);
                

                //opcion por defecto combobox
                Rol defaultCB = new Rol
                {
                    rol_id = 0,
                    rol_desc = "Seleccionar",
                    rol_index = 0
                };
                //insertar en la primera posición
                listaCB.Insert(0, defaultCB);

                PerfilCB.ItemsSource = listaCB;

                //datos menuitem por id
                var usuario = await usuarioDao.GetById(this.usuario_id);
                //obtener el nombre de Rol
                string nombreRol = (from c in listaCB
                                          where c.rol_id == usuario.rol_id
                                          select new
                                          {
                                              c.rol_desc
                                          }).FirstOrDefault().rol_desc;

                //identificar la posicion en el combobox
                int indice = 0;

                for (int i = 0; i < listaCB.Count; i++)
                {
                    string opcion = listaCB[i].rol_desc;
                    if (opcion.Equals(nombreRol))
                        indice = i;
                }


                PerfilCB.SelectedIndex = indice;
                txtNombre.Text = usuario.nombre;
                txtPaterno.Text = usuario.ap_paterno;
                txtMaterno.Text = usuario.ap_materno;
                txtEmail.Text = usuario.e_mail;
                txtFono.Text = usuario.fono;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar datos en Actualizar");
            }
        }
    }
}

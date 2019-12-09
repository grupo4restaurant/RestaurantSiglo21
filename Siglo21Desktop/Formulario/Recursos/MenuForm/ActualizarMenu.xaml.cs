using Caliburn.Micro;
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

namespace Siglo21Desktop.Formulario.Recursos.MenuForm
{
    /// <summary>
    /// Interaction logic for ActualizarMenu.xaml
    /// </summary>
    public partial class ActualizarMenu : Window
    {
        private int item_id;
        public ActualizarMenu(int item_id)
        {
            this.item_id = item_id;
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void categoriaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //CategoriaMenu selected = this.People.SelectedItem as CategoriaMenu;
            //MessageBox.Show(selected.cat_menu_nombre);
        }

        private async void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            CategoriaMenu selectedCategoria = this.categoriaCB.SelectedItem as CategoriaMenu;
            string nombre;
            string descripcion;
            nombre = txtNombre.Text;
            descripcion = txtDescripcion.Text;
            int valor = Int32.Parse(txtValor.Text);
            

            MenuItemDAO dao = new MenuItemDAO();
            try
            {
                Entities.MenuItem obj = new Entities.MenuItem()
                {
                    item_id = this.item_id,
                    cat_menu_id = selectedCategoria.cat_menu_id,
                    item_nombre = nombre,
                    item_desc = descripcion,
                    item_val = valor
                };
                var response = await dao.Update(obj);

                MessageBox.Show("Item Menú Actualizado Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Item Menú no Actualizado");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CategoriaMenuDAO categoriaMenuDao = new CategoriaMenuDAO();
            MenuItemDAO menuItemDao = new MenuItemDAO();
            try
            {
                //datos combobox
                var combobox = await categoriaMenuDao.GetAll();
                BindableCollection<CategoriaMenu> lista = new BindableCollection<CategoriaMenu>(combobox);
                categoriaCB.ItemsSource = lista;

                //opcion por defecto combobox
                CategoriaMenu defaultCB = new CategoriaMenu
                {
                    cat_menu_id = 0,
                    cat_menu_nombre = "Seleccionar",
                    cat_fase = 0
                };
                //insertar en la primera posición
                lista.Insert(0, defaultCB);

                //datos menuitem por id
                var menuItem = await menuItemDao.GetById(this.item_id);
                //obtener el nombre de la categoria
                string nombreCategoria = (from c in lista
                                       where c.cat_menu_id == menuItem.cat_menu_id
                                       select new
                                       {
                                           c.cat_menu_nombre
                                       }).FirstOrDefault().cat_menu_nombre;

                //identificar la posicion en el combobox
                int indice = 0;
                
                for(int i=0; i<lista.Count; i++)
                {
                    string opcion = lista[i].cat_menu_nombre;
                    if (opcion.Equals(nombreCategoria))
                        indice = i;
                }

                
                categoriaCB.SelectedIndex = indice;
                txtNombre.Text = menuItem.item_nombre;
                txtDescripcion.Text = menuItem.item_desc;
                txtValor.Text = menuItem.item_val.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar datos en Actualizar");
            }
        }
    }
}

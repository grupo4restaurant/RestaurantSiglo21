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
    /// Interaction logic for IngresoMenu.xaml
    /// </summary>
    public partial class IngresoMenu : Window
    {
        public IngresoMenu()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void categoriaCB_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            //CategoriaMenu selected = this.People.SelectedItem as CategoriaMenu;
            //MessageBox.Show(selected.cat_menu_nombre);
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            CategoriaMenu selectedCategoria = this.categoriaCB.SelectedItem as CategoriaMenu;
            string nombre;
            string descripcion;
            int valor;
            nombre = txtNombre.Text;
            descripcion = txtDescripcion.Text;
            valor = Int32.Parse(txtValor.Text);

            MenuItemDAO dao = new MenuItemDAO();
            try
            {
                Entities.MenuItem obj = new Entities.MenuItem()
                {
                    cat_menu_id = selectedCategoria.cat_menu_id,
                    item_nombre = nombre,
                    item_desc =descripcion,
                    item_val = valor
                };
                var response = await dao.Save(obj);

                MessageBox.Show("Item Menú Añadido Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Item Menú no Añadido, Podría Existir Duplicidad de ID");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CategoriaMenuDAO categoriaMenuDao = new CategoriaMenuDAO();
            try
            {
                var result = await categoriaMenuDao.GetAll();
                BindableCollection<CategoriaMenu> lista = new BindableCollection<CategoriaMenu>(result);

                //opcion por defecto combobox
                CategoriaMenu defaultCB = new CategoriaMenu
                {
                    cat_menu_id = 0,
                    cat_menu_nombre = "Seleccionar",
                    cat_fase = 0
                };
                //insertar en la primera posición
                lista.Insert(0, defaultCB);

                categoriaCB.ItemsSource = lista;
                categoriaCB.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar listado Categoría");
            }
        }
    }
}

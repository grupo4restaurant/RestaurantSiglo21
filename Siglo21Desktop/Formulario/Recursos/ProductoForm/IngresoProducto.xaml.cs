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

namespace Siglo21Desktop.Formulario.Recursos.ProductoForm
{
    /// <summary>
    /// Interaction logic for IngresoProducto.xaml
    /// </summary>
    public partial class IngresoProducto : Window
    {
        public IngresoProducto()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }


        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            CatalogoProducto selectedCatalogo = this.CatalogoCB.SelectedItem as CatalogoProducto;
            Proveedor selectedProveedor = this.ProveedorCB.SelectedItem as Proveedor;

            int cat_prod_id = selectedCatalogo.cat_prod_id;
            int proveedor_id = selectedProveedor.proveedor_id;
            string nombre = txtNombre.Text;
            string cod = txtCod.Text;
            string valor_neto = txtValor.Text;
            


            ProductoDAO dao = new ProductoDAO();
            
            try
            {
                Producto obj = new Producto()
                {
                    cat_prod_id = cat_prod_id,
                    proveedor_id = proveedor_id,
                    nombre = nombre,
                    cod = cod,
                    valor_neto = Int32.Parse(valor_neto)
                };
                var response = await dao.Save(obj);

                
                MessageBox.Show("Producto Añadido Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Producto no Añadido, Podría Existir Duplicidad de ID");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CatalogoProductoDAO catalogoDao = new CatalogoProductoDAO();
            ProveedorDAO proveedorDao = new ProveedorDAO();
            try
            {
                //insertar datos al combobox CatalogoCB
                var catalogoResult = await catalogoDao.GetAll();
                BindableCollection<CatalogoProducto> listaCatalogo = new BindableCollection<CatalogoProducto>(catalogoResult);

                //opcion por defecto combobox
                CatalogoProducto defaultCB1 = new CatalogoProducto
                {
                    cat_prod_id = 0,
                    nombre = "Seleccionar"
                };
                //insertar en la primera posición
                listaCatalogo.Insert(0, defaultCB1);

                CatalogoCB.ItemsSource = listaCatalogo;
                CatalogoCB.SelectedIndex = 0;

                //insertar datos al combobox ProveedorCB
                var proveedorResult = await proveedorDao.GetAll();
                BindableCollection<Proveedor> listaProveedor = new BindableCollection<Proveedor>(proveedorResult);

                //opcion por defecto combobox
                Proveedor defaultCB2 = new Proveedor
                {
                    proveedor_id = 0,
                    nombre = "Seleccionar"
                };
                //insertar en la primera posición
                listaProveedor.Insert(0, defaultCB2);

                ProveedorCB.ItemsSource = listaProveedor;
                ProveedorCB.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar listados");
            }
        }
    }
}
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
    /// Interaction logic for ActualizarProducto.xaml
    /// </summary>
    public partial class ActualizarProducto : Window
    {
        private int producto_id;
        public ActualizarProducto(int producto_id)
        {
            this.producto_id = producto_id;
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }


        private async void btnActualizar_Click(object sender, RoutedEventArgs e)
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
                    producto_id = this.producto_id,
                    cat_prod_id = cat_prod_id,
                    proveedor_id = proveedor_id,
                    nombre = nombre,
                    cod = cod,
                    valor_neto = Int32.Parse(valor_neto)
                };
                var response = await dao.Update(obj);

                MessageBox.Show("Producto Actualizado Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Producto no Actualizado");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CatalogoProductoDAO catalogoDao = new CatalogoProductoDAO();
            ProveedorDAO proveedorDao = new ProveedorDAO();
            ProductoDAO productoDao = new ProductoDAO();
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
                

                //datos producto por id
                var producto = await productoDao.GetById(this.producto_id);
                //obtener el nombre de Rol
                string nombreCatalogo = (from c in listaCatalogo
                                    where c.cat_prod_id == producto.cat_prod_id
                                    select new
                                    {
                                        c.nombre
                                    }).FirstOrDefault().nombre;
                //datos proveedor por id
                var proveedor = await productoDao.GetById(this.producto_id);
                //obtener el nombre de Rol
                string nombreProveedor = (from c in listaProveedor
                                          where c.proveedor_id == proveedor.proveedor_id
                                          select new
                                         {
                                             c.nombre
                                         }).FirstOrDefault().nombre;


                //identificar la posicion en el combobox CatalogoCB
                int indiceCatalogo = 0;

                for (int i = 0; i < listaCatalogo.Count; i++)
                {
                    string opcion = listaCatalogo[i].nombre;
                    if (opcion.Equals(nombreCatalogo))
                        indiceCatalogo = i;
                }

                //identificar la posicion en el combobox CatalogoCB
                int indiceProveedor = 0;

                for (int i = 0; i < listaProveedor.Count; i++)
                {
                    string opcion = listaProveedor[i].nombre;
                    if (opcion.Equals(nombreProveedor))
                        indiceProveedor = i;
                }

                CatalogoCB.SelectedIndex = indiceCatalogo;
                ProveedorCB.SelectedIndex = indiceProveedor;
                txtNombre.Text = producto.nombre;
                txtCod.Text = producto.cod;
                txtValor.Text = producto.valor_neto.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar datos en Actualizar");
            }
        }
    }
}
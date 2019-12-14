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

namespace Siglo21Desktop.Formulario.Bodega.Almacenamiento
{
    /// <summary>
    /// Interaction logic for EditarForm.xaml
    /// </summary>
    public partial class EditarForm : Window
    {
        private int producto_id;
        public EditarForm(int producto_id)
        {
            this.producto_id = producto_id;
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }




        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CatalogoProductoDAO catalogoDao = new CatalogoProductoDAO();
            ProveedorDAO proveedorDao = new ProveedorDAO();
            ProductoDAO productoDao = new ProductoDAO();
            StockProductoDAO stockDao = new StockProductoDAO();

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

                var stock = await stockDao.GetById(this.producto_id);

                CatalogoCB.SelectedIndex = indiceCatalogo;
                ProveedorCB.SelectedIndex = indiceProveedor;
                txtNombre.Text = producto.nombre;
                txtCod.Text = producto.cod;
                txtStock.Text = (stock.cantidad > 0) ? stock.cantidad.ToString() : (0).ToString();
                txtMinimo.Text = (stock.minimo > 0) ? stock.minimo.ToString() : (0).ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar datos en Editar Stock");
            }
        }

        private async void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            int? cantidad = Int32.Parse(txtCantidad.Text);
            int? minimo = Int32.Parse(txtMinimo.Text);
            if ((cantidad!= null && cantidad >= 0)&& (minimo != null && minimo >= 0))
            {
                StockProductoDAO stockDao = new StockProductoDAO();
                var obj = await stockDao.GetById(this.producto_id);
                int suma = obj.cantidad + (int)cantidad;

                StockProducto stock = new StockProducto
                {
                    producto_id = this.producto_id,
                    cantidad = suma,
                    minimo = (int)minimo
                };

                try
                {
                    var result = await stockDao.Update(stock);
                    MessageBox.Show("Stock Editado Exitosamente");
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error Editar Stock");
                    this.Close();
                }
            }            
            else
            {
                MessageBox.Show("Valor no válido!");
                
            }
            
        }

        private async void btnRebajar_Click(object sender, RoutedEventArgs e)
        {
            int? cantidad = Int32.Parse(txtCantidad.Text);
            int? minimo = Int32.Parse(txtMinimo.Text);
            if ((cantidad != null && cantidad >= 0) && (minimo != null && minimo >= 0))
            {
                if ((int)cantidad < Int32.Parse(txtStock.Text))
                {
                    StockProductoDAO stockDao = new StockProductoDAO();
                    var obj = await stockDao.GetById(this.producto_id);
                    int suma = obj.cantidad - (int)cantidad;

                    StockProducto stock = new StockProducto
                    {
                        producto_id = this.producto_id,
                        cantidad = suma,
                        minimo = (int)minimo
                    };

                    try
                    {
                        var result = await stockDao.Update(stock);
                        MessageBox.Show("Stock Editado Exitosamente");
                        this.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error Editar Stock");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Cantidad Excede el Stock disponible");
                }
                
            }
            else
            {
                MessageBox.Show("Valor no válido!");

            }



        }

        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }



    }
}

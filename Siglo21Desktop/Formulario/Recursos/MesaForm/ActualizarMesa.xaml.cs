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

namespace Siglo21Desktop.Formulario.Recursos.MesaForm
{
    /// <summary>
    /// Interaction logic for ActualizarMesa.xaml
    /// </summary>
    public partial class ActualizarMesa : Window
    {
        private int mesa_id;
        public ActualizarMesa(int mesa_id)
        {
            this.mesa_id = mesa_id;
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        
        private async void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Domino selectedEstado = this.estadoMesaCB.SelectedItem as Domino;

            string mesa = (txtMesa.Text).ToUpper();
            string capacidad = txtCapacidad.Text;

            MesaDAO dao = new MesaDAO();
            //var listadoMesa = await dao.GetAll();
            //var result = (from u in listadoMesa
            //              where u.mesa_numero == mesa
            //              select new
            //              {
            //                  u.mesa_id
            //              }).FirstOrDefault();

            //if (result != null)
            //{

            //    MessageBox.Show("Mesa ya Existe");
            //    this.Close();

            //}

            //else

                try
            {
                Mesa obj = new Mesa()
                {
                    mesa_id = this.mesa_id,
                    mesa_numero = mesa,
                    mesa_estado = selectedEstado.dom_val,
                    mesa_capacidad = Int32.Parse(capacidad)
                };
                var response = await dao.Update(obj);

                MessageBox.Show("Mesa Actualizado Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mesa no Actualizado");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DominioDAO domDao = new DominioDAO();
            MesaDAO mesaDao = new MesaDAO();
            try
            {
                //datos combobox
                var combobox = await domDao.GetAllByDomValDom("Mesa");
                BindableCollection<Domino> lista = new BindableCollection<Domino>(combobox);
                
                //opcion por defecto combobox
                Domino defaultCB = new Domino
                {
                    dom_desc = "Seleccionar",
                    dom_val = 0
                };
                //insertar en la primera posición
                lista.Insert(0, defaultCB);

                estadoMesaCB.ItemsSource = lista;

                //datos menuitem por id
                var mesa = await mesaDao.GetById(this.mesa_id);
                //obtener el descripcion de estado
                string estadoMesa = (from c in lista
                                          where c.dom_val == mesa.mesa_estado
                                          select new
                                          {
                                              c.dom_desc
                                          }).FirstOrDefault().dom_desc;

                //identificar la posicion en el combobox
                int indice = 0;

                for (int i = 0; i < lista.Count; i++)
                {
                    string opcion = lista[i].dom_desc;
                    if (opcion.Equals(estadoMesa))
                        indice = i;
                }


                estadoMesaCB.SelectedIndex = indice;
                txtMesa.Text = mesa.mesa_numero;
                txtCapacidad.Text = mesa.mesa_capacidad.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar datos en Actualizar");
            }
        }
    }
}
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
    /// Interaction logic for IngresoMesa.xaml
    /// </summary>
    public partial class IngresoMesa : Window
    {
        public IngresoMesa()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Domino selectedEstado = this.estadoMesaCB.SelectedItem as Domino;
            
            string mesa = (txtMesa.Text).ToUpper();
            string capacidad = txtCapacidad.Text;

            MesaDAO dao = new MesaDAO();
            try
            {
                Mesa obj = new Mesa()
                {
                    mesa_numero = mesa,
                    mesa_estado = selectedEstado.dom_val,
                    mesa_capacidad = Int32.Parse(capacidad)
                };
                var response = await dao.Save(obj);

                MessageBox.Show("Mesa Añadido Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mesa no Añadido, Podría Existir Duplicidad de ID");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DominioDAO dominioDao = new DominioDAO();
            try
            {
                var result = await dominioDao.GetAllByDomValDom("Mesa");
                
                BindableCollection<Domino> lista = new BindableCollection<Domino>(result);

                //opcion por defecto combobox
                Domino defaultCB = new Domino
                {
                    dom_val_dom = "Mesa",
                    dom_desc = "Seleccionar",
                    dom_val = 0
                };
                //insertar en la primera posición
                lista.Insert(0, defaultCB);

                estadoMesaCB.ItemsSource = lista;
                estadoMesaCB.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar listado Estado al ComboBox");
            }
        }
    }
}


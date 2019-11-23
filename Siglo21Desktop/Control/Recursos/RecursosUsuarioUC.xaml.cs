using Newtonsoft.Json;
using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Siglo21Desktop.Control.Recursos
{
    /// <summary>
    /// Interaction logic for RecursosUsuarioUC.xaml
    /// </summary>
    public partial class RecursosUsuarioUC : UserControl
    {
        
        public RecursosUsuarioUC()
        {
            InitializeComponent();
            
        }

        private async void Guardar_Button_Click(object sender, RoutedEventArgs e) 
        {
            RolDAO rolDao = new RolDAO();
            try
            {
                Rol obj = new Rol()
                {
                    //rol_id = 21,
                    rol_desc = "Test8",
                    rol_index = 31
                };
                var response = await rolDao.Save(obj); 
                
                MessageBox.Show("Rol Añadido Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Rol no Añadido, Podría Existir Duplicidad de ID");
            }
        }

        private async void Actualizar_Button_Click(object sender, RoutedEventArgs e) 
        {
            RolDAO rolDao = new RolDAO();
            try
            {
                Rol obj = new Rol()
                {
                    rol_id = 21,
                    rol_desc = "Test5",
                    rol_index = 20
                };
                var response = await rolDao.Update(obj);
                
                MessageBox.Show("Rol Actualizado Exitosamente", "Result", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Rol no Actualizado, Podría no Existir el ID");
            }
        }

        private async void Borrar_Button_Click(object sender, RoutedEventArgs e)
        {
            RolDAO rolDao = new RolDAO();
            try
            {
                var response = await rolDao.Delete(101);
                if (response)
                {
                    MessageBox.Show("Rol Exitosamente Borrado!");
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Rol no Borrado!");
            }
        }

        private async void Obtener_Button_Click(object sender, RoutedEventArgs e) 
        {
            RolDAO rolDao = new RolDAO();
            try
            {                
                var result = await rolDao.GetById(1);
            }
            catch (Exception)
            {
                MessageBox.Show("Rol no Encontrado");
            }

        }

        


    }    
}

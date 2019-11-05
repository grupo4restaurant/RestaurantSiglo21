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
        

        //[JsonArray]
        //public class Rols { public List<Rol> JSON; } 

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
                    rol_desc = "Test6",
                    rol_index = 30
                };
                var response = await rolDao.Save(obj); 
                response.EnsureSuccessStatusCode(); // Throw on error code.
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
                response.EnsureSuccessStatusCode(); // Throw on error code.
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
                HttpResponseMessage response = await rolDao.Delete(61);
                response.EnsureSuccessStatusCode(); // Throw on error code.
                MessageBox.Show("Rol Exitosamente Borrado!");
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
                //MessageBox.Show("Rol Encontrado!");
            }
            catch (Exception)
            {
                MessageBox.Show("Rol no Encontrado");
            }

        }

        


    }    
}

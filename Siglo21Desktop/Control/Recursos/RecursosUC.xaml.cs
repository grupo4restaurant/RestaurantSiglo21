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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Siglo21Desktop.Control.Recursos
{
    /// <summary>
    /// Interaction logic for RecursosUC.xaml
    /// </summary>
    public partial class RecursosUC : UserControl
    {
        public RecursosUC()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (180 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    GridMain.Background = Brushes.Aquamarine;
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new RecursosProductoUC());
                    break;
                case 1:
                    GridMain.Background = Brushes.Beige;
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new RecursosProveedorUC());
                    break;
                case 2:
                    GridMain.Background = Brushes.CadetBlue;
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new RecursosUsuarioUC());
                    break;
                case 3:
                    GridMain.Background = Brushes.DarkBlue;
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new RecursosMesaUC());
                    break;
                case 4:
                    GridMain.Background = Brushes.DarkBlue;
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new RecursosMenuUC());
                    break;

            }
        }
    }
}

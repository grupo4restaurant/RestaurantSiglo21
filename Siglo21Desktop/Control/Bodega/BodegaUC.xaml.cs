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

namespace Siglo21Desktop.Control.Bodega
{
    /// <summary>
    /// Interaction logic for BodegaUC.xaml
    /// </summary>
    public partial class BodegaUC : UserControl
    {
        public BodegaUC()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (250 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    GridMain.Background = Brushes.Aquamarine;
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new BodegaAlmacenamientoUC());
                    break;
                case 1:
                    GridMain.Background = Brushes.Beige;
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new BodegaRecepcionUC());
                    break;
                case 2:
                    GridMain.Background = Brushes.Beige;
                    GridMain.Children.Clear();
                    GridMain.Children.Add(new BodegaOrdenCompraUC());
                    break;
                
            }
        }
    }
}

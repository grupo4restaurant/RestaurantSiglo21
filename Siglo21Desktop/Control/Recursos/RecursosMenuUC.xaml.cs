using Siglo21Desktop.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for RecursosMenuUC.xaml
    /// </summary>
    public partial class RecursosMenuUC : UserControl
    {
        public RecursosMenuUC()
        {
            InitializeComponent();
            //create business data
            var itemList = new List<StockItem>();
            itemList.Add(new StockItem { Name = "Many items", Quantity = 100, IsObsolete = false });
            itemList.Add(new StockItem { Name = "Enough items", Quantity = 10, IsObsolete = false });
            itemList.Add(new StockItem { Name = "Shortage item", Quantity = 1, IsObsolete = false });
            itemList.Add(new StockItem { Name = "Item with error", Quantity = -1, IsObsolete = false });
            itemList.Add(new StockItem { Name = "Obsolete item", Quantity = 200, IsObsolete = true });

            //link business data to CollectionViewSource
            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)(FindResource("ItemCollectionViewSource"));
            itemCollectionViewSource.Source = itemList;
        }

        
        
        
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StockItem dataRowView = (StockItem)((Button)e.Source).DataContext;
                String ProductName = dataRowView.Name;
                String ProductDescription = dataRowView.Quantity.ToString();
                MessageBox.Show("You Clicked : " + ProductName + "\r\nDescription : " + ProductDescription);
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StockItem dataRowView = (StockItem)((Button)e.Source).DataContext;
                String ProductName = dataRowView.Name;
                String ProductDescription = dataRowView.Quantity.ToString();
                MessageBox.Show("You Clicked : " + ProductName + "\r\nDescription : " + ProductDescription + "\r\nBorrado!!!");
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }






    }
}
    

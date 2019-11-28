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
        int pageIndex = 1;
        private int numberOfRecPerPage;
        //To check the paging direction according to use selection.
        private enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };

        List<StockItem> myList = new List<StockItem>();

        public RecursosMenuUC()
        {
            InitializeComponent();
            //create business data
            //var itemList = new List<StockItem>();
            //itemList.Add(new StockItem { Name = "Many items", Quantity = 100, IsObsolete = false });
            //itemList.Add(new StockItem { Name = "Enough items", Quantity = 10, IsObsolete = false });
            //itemList.Add(new StockItem { Name = "Shortage item", Quantity = 1, IsObsolete = false });
            //itemList.Add(new StockItem { Name = "Item with error", Quantity = -1, IsObsolete = false });
            //itemList.Add(new StockItem { Name = "Obsolete item", Quantity = 200, IsObsolete = true });

            cbNumberOfRecords.Items.Add("10");
            cbNumberOfRecords.Items.Add("20");
            cbNumberOfRecords.Items.Add("30");
            cbNumberOfRecords.Items.Add("50");
            cbNumberOfRecords.Items.Add("100");
            cbNumberOfRecords.SelectedItem = 10;
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            myList = GetData();
            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
            int count = myList.Take(numberOfRecPerPage).Count();
            lblpageInformation.Content = count + " of " + myList.Count;
            dataGrid.Columns[0].Header = "Primero";
            dataGrid.Columns[0].Header = "Segundo";
            dataGrid.Columns[0].Header = "Tercero";
        }

        private List<StockItem> GetData()
        {
            List<StockItem> genericList = new List<StockItem>();
            StockItem studentObj;
            Random randomObj = new Random();
            for (int i = 0; i < 1000; i++)
            {
                studentObj = new StockItem();
                studentObj.Name = "First " + i;
                studentObj.Quantity = (int)randomObj.Next(1, 100);
                studentObj.IsObsolete = true;

                genericList.Add(studentObj);
            }
            return genericList;
        }

        //private void btnCancel_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        #region Pagination 
        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.First);
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Next);

        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Previous);

        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Last);
        }

        private void cbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate((int)PagingMode.PageCountChange);
        }

        private void Navigate(int mode)
        {
            int count;
            switch (mode)
            {
                case (int)PagingMode.Next:
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    if (myList.Count >= (pageIndex * numberOfRecPerPage))
                    {
                        if (myList.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = myList.Skip((pageIndex * numberOfRecPerPage) - numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) + (myList.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                        }
                        else
                        {
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = myList.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) + (myList.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                            pageIndex++;
                        }

                        lblpageInformation.Content = count + " of " + myList.Count;
                    }

                    else
                    {
                        btnNext.IsEnabled = false;
                        btnLast.IsEnabled = false;
                    }

                    break;
                case (int)PagingMode.Previous:
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    if (pageIndex > 1)
                    {
                        pageIndex -= 1;
                        dataGrid.ItemsSource = null;
                        if (pageIndex == 1)
                        {
                            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
                            count = myList.Take(numberOfRecPerPage).Count();
                            lblpageInformation.Content = count + " of " + myList.Count;
                        }
                        else
                        {
                            dataGrid.ItemsSource = myList.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = Math.Min(pageIndex * numberOfRecPerPage, myList.Count);
                            lblpageInformation.Content = count + " of " + myList.Count;
                        }
                    }
                    else
                    {
                        btnPrev.IsEnabled = false;
                        btnFirst.IsEnabled = false;
                    }
                    break;

                case (int)PagingMode.First:
                    pageIndex = 2;
                    Navigate((int)PagingMode.Previous);
                    break;
                case (int)PagingMode.Last:
                    pageIndex = (myList.Count / numberOfRecPerPage);
                    Navigate((int)PagingMode.Next);
                    break;

                case (int)PagingMode.PageCountChange:
                    pageIndex = 1;
                    numberOfRecPerPage = Convert.ToInt32(cbNumberOfRecords.SelectedItem);
                    dataGrid.ItemsSource = null;
                    dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
                    count = (myList.Take(numberOfRecPerPage)).Count();
                    lblpageInformation.Content = count + " of " + myList.Count;
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    break;
            }
        }

        #endregion



        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    

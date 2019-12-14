using Caliburn.Micro;
using Siglo21Desktop.Dao;
using Siglo21Desktop.Entities;
using Siglo21Desktop.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Siglo21Desktop.Helpers
{
    class PaginacionMesa : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Fields

        private ObservableCollection<MesaModel> _listado; 

        private int start = 0;
        //paginacion de 5 items(rows)
        private int itemCount = 5;
        //variable get de la entidad
        private string sortColumn = "Tipo1";

        private bool ascending = true;

        private int totalItems = 0;

        private ICommand firstCommand;

        private ICommand previousCommand;

        private ICommand nextCommand;

        private ICommand lastCommand;

        #endregion

        /// <summary>
        /// Constructor. Initializes the list of products.
        /// </summary>
        public PaginacionMesa()
        {
            RefreshProducts();
        }

        /// <summary>
        /// The list of products in the current page.
        /// </summary>
        public ObservableCollection<MesaModel> Listado
        {
            get
            {
                return _listado;
            }
            private set
            {
                if (object.ReferenceEquals(_listado, value) != true)
                {
                    _listado = value;
                    //nombre del binding en design en la propiedad ItemsSource
                    NotifyPropertyChanged("Listado");
                }
            }
        }

        /// <summary>
        /// Gets the index of the first item in the products list.
        /// </summary>
        public int Start { get { return start + 1; } }

        /// <summary>
        /// Gets the index of the last item in the products list.
        /// </summary>
        public int End { get { return start + itemCount < totalItems ? start + itemCount : totalItems; } }

        /// <summary>
        /// The number of total items in the data store.
        /// </summary>
        public int TotalItems { get { return totalItems; } }

        /// <summary>
        /// Gets the command for moving to the first page of products.
        /// </summary>
        public ICommand FirstCommand
        {
            get
            {
                if (firstCommand == null)
                {
                    firstCommand = new RelayCommand
                    (
                        param =>
                        {
                            start = 0;
                            RefreshProducts();
                        },
                        param =>
                        {
                            return start - itemCount >= 0 ? true : false;
                        }
                    );
                }

                return firstCommand;
            }
        }

        /// <summary>
        /// Gets the command for moving to the previous page of products.
        /// </summary>
        public ICommand PreviousCommand
        {
            get
            {
                if (previousCommand == null)
                {
                    previousCommand = new RelayCommand
                    (
                        param =>
                        {
                            start -= itemCount;
                            RefreshProducts();
                        },
                        param =>
                        {
                            return start - itemCount >= 0 ? true : false;
                        }
                    );
                }

                return previousCommand;
            }
        }

        /// <summary>
        /// Gets the command for moving to the next page of products.
        /// </summary>
        public ICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RelayCommand
                    (
                        param =>
                        {
                            start += itemCount;
                            RefreshProducts();
                        },
                        param =>
                        {
                            return start + itemCount < totalItems ? true : false;
                        }
                    );
                }

                return nextCommand;
            }
        }

        /// <summary>
        /// Gets the command for moving to the last page of products.
        /// </summary>
        public ICommand LastCommand
        {
            get
            {
                if (lastCommand == null)
                {
                    lastCommand = new RelayCommand
                    (
                        param =>
                        {
                            start = (totalItems / itemCount - 1) * itemCount;
                            start += totalItems % itemCount == 0 ? 0 : itemCount;
                            RefreshProducts();
                        },
                        param =>
                        {
                            return start + itemCount < totalItems ? true : false;
                        }
                    );
                }

                return lastCommand;
            }
        }

        /// <summary>
        /// Sorts the list of products.
        /// </summary>
        /// <param name="sortColumn">The column or member that is the basis for sorting.</param>
        /// <param name="ascending">Set to true if the sort</param>
        public void Sort(string sortColumn, bool ascending)
        {
            this.sortColumn = sortColumn;
            this.ascending = ascending;

            RefreshProducts();
        }

        /// <summary>
        /// Refreshes the list of products. Called by navigation commands.
        /// </summary>
        private async void RefreshProducts()
        {
            MesaDAO mesaDao = new MesaDAO();
            try
            {
                var result = await mesaDao.GetAll();

                List<MesaModel> listaMesaModel = new List<MesaModel>();

                foreach(var item in result)
                {
                    string estado;
                    if (item.mesa_estado == 1)
                        estado = "Deshabilitado";
                    else
                        estado = "Habilitado";

                    listaMesaModel.Add(new MesaModel {
                        mesa_id = item.mesa_id,
                        mesa_numero = item.mesa_numero,
                        mesa_estado = item.mesa_estado,
                        mesa_capacidad = item.mesa_capacidad,
                        mesa_numero_descripcion = estado
                    });
                }



                BindableCollection<MesaModel> lista = new BindableCollection<MesaModel>(listaMesaModel);
                Listado = DataAccess.GetMesa(start, itemCount, sortColumn, ascending, out totalItems, lista);

                NotifyPropertyChanged("Start");
                NotifyPropertyChanged("End");
                NotifyPropertyChanged("TotalItems");
            }
            catch (Exception)
            {
                MessageBox.Show("Listado Mesa no Encontrado(paginacion)");
            }

        }

        /// <summary>
        /// Notifies subscribers of changed properties.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
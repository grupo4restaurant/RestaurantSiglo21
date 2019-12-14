using Caliburn.Micro;
using Siglo21Desktop.Dao;
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
    class PaginacionUsuario : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Fields

        private ObservableCollection<UsuarioModel> _listado;

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
        public PaginacionUsuario() 
        {
            RefreshProducts();
        }

        /// <summary>
        /// The list of products in the current page.
        /// </summary>
        public ObservableCollection<UsuarioModel> Listado
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
            UsuarioDAO usuarioDao = new UsuarioDAO();
            RolDAO rolDao = new RolDAO();

            try
            {
                var usuarioResult = await usuarioDao.GetAll();
                var rolResult = await rolDao.GetAll();
                var resultJoin = (from u in usuarioResult
                              join r in rolResult on u.rol_id equals r.rol_id
                              select new
                              {
                                  u.usuario_id,
                                  u.rol_id,
                                  u.nombre,
                                  u.ap_paterno,
                                  u.ap_materno,
                                  u.e_mail,
                                  u.fono,
                                  r.rol_desc,
                                  r.rol_index
                              }).ToList();

                List<UsuarioModel> listaUsuarioModel = new List<UsuarioModel>();

                foreach(var item in resultJoin)
                {
                    listaUsuarioModel.Add(new UsuarioModel
                    {
                        usuario_id = item.usuario_id,
                        rol_id = item.rol_id,
                        nombre = item.nombre,
                        ap_paterno = item.ap_paterno,
                        ap_materno = item.ap_materno,
                        e_mail = item.e_mail,
                        fono = item.fono,
                        rol_desc = item.rol_desc,
                        rol_index = item.rol_index
                    });
                }


                BindableCollection<UsuarioModel> lista = new BindableCollection<UsuarioModel>(listaUsuarioModel);
                Listado = DataAccess.GetUsuarios(start, itemCount, sortColumn, ascending, out totalItems, lista);

                NotifyPropertyChanged("Start");
                NotifyPropertyChanged("End");
                NotifyPropertyChanged("TotalItems");
            }
            catch (Exception)
            {
                MessageBox.Show("Listado Usuario no Encontrado(paginacion)");
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
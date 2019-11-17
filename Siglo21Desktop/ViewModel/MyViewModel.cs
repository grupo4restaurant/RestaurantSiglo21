using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siglo21Desktop.ViewModel
{
    class MyViewModel : ViewModelBase
    {
        private DataTable datatable;
        public DataTable DataTable
        {
            get
            {
                if (this.datatable == null)
                {
                    this.datatable = this.CreateDataTable();
                }

                return this.datatable;
            }
        }
        private DataTable CreateDataTable()
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(new System.Data.DataColumn("FirstName", typeof(string)));
            dataTable.Columns.Add(new System.Data.DataColumn("LastName", typeof(string)));
            dataTable.Columns.Add(new System.Data.DataColumn("City", typeof(string)));

            for (int i = 1; i <= 50; i++)
            {
                var row = dataTable.NewRow();
                row["FirstName"] = "FirstName " + i;
                row["LastName"] = "LastName" + i;
                row["City"] = "City " + i;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}

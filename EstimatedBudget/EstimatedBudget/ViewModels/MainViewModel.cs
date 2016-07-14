using System;
using EstimatedBudget.POCO;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;

namespace EstimatedBudget.ViewModels
{
    public partial class MainViewModel : ViewModelBase, IDataErrorInfo
    {

        public MainViewModel()
        {
            //Check IF Databse Exist
            if (!File.Exists(new ConnectionProvider().GetOpenConnection().ConnectionString))
            {
               DataBaseManager.CreateDatabase();
            }
        }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

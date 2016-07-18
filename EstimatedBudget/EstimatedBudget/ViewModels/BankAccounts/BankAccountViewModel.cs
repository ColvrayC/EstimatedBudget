using EstimatedBudget.Helpers;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using EstimatedBudget.POCO.Models;
using EstimatedBudget.POCO.DAL;

namespace EstimatedBudget.ViewModels.BankAccounts
{
    public class BankAccountViewModel : ViewModelBase
    {
        public BankAccountViewModel()
        {
            CurrentMode = EditionMode.DEFAULT;
            BankAccounts = new ObservableCollection<BankAccount>(BankAccountDAL.Load());
        }

        [RaisePropertyChanged]
        public virtual string CurrentMode
        {
            get { return _CurrentMode; }
            set { _CurrentMode = value; if (_CurrentMode == EditionMode.DEFAULT) SelectedIndex = -1; }
        }
        private string _CurrentMode;

        [RaisePropertyChanged]
        public virtual int SelectedIndex { get; set; }

        [RaisePropertyChanged]
        public virtual BankAccount SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; CurrentMode = EditionMode.MODIFICATION; }
        }
        private BankAccount _SelectedItem;

        [RaisePropertyChanged]
        public virtual ObservableCollection<BankAccount> BankAccounts { get; set; }
    }
}

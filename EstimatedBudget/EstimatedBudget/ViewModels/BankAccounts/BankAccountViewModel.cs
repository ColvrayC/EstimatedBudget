using EstimatedBudget.Helpers;
using EstimatedBudget.POCO.DAL;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using EstimatedBudget.POCO.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.ComponentModel;

namespace EstimatedBudget.ViewModels.BankAccounts
{
    public partial class BankAccountViewModel : ViewModelBase, IDataErrorInfo
    {
        public BankAccountViewModel()
        {
            //Initialisation
            SelectedIndex = -1;
            ActiveMode = Modes.DEFAULT;
            BankAccounts = new ObservableCollection<BankAccount>(BankAccountDAL.Load());

            //Commands
            ValidatedCommand = new RelayCommand<int>(Validated);
            ModeCommand = new RelayCommand<string>(ChangeMode);
            RequestDeleteCommand = new RelayCommand(RequestDelete);
        }

        /// <summary>
        /// METHODS
        /// </summary>
        public async void Validated(int index)
        {
            if(IsValid)
            {
            if (ActiveMode == Modes.ADD)
                {
                    BankAccountDAL.Save(SpecsBankAccount);
                    BankAccounts.Add(SpecsBankAccount);
                }
                else
                {
                    BankAccountDAL.Update(SpecsBankAccount);
                    BankAccounts[index] = SpecsBankAccount; 
                }
                ActiveMode = Modes.DEFAULT;
            }
            else
                await DialogService.ShowMessage2(DialogService.TitleTextErrorValidation, DialogService.TextErrorValidation, MessageDialogStyle.Affirmative);

        }

        public void ChangeMode(string Mode)
        {
            ActiveMode = Mode;
            if (ActiveMode == Modes.ADD)
                SpecsBankAccount = new BankAccount();
               // SpecsBankAccount = null;
            if (ActiveMode == Modes.DEFAULT)
                SelectedIndex = -1;
        }

        public async void RequestDelete()
        {
            var Result = await DialogService.ShowMessage(DialogService.TitleTextConfirmDelete, DialogService.TextConfirmDelete, MessageDialogStyle.AffirmativeAndNegative);
            if (Result == MessageDialogResult.Affirmative)
            {
                BankAccountDAL.Delete(SpecsBankAccount);
                ActiveMode = Modes.DEFAULT;
                BankAccounts = new ObservableCollection<BankAccount>(BankAccountDAL.Load());
            }
        }
       

        /// <summary>
        /// PROPERTIES
        /// </summary>

        //Active Modes
        [RaisePropertyChanged]
        public virtual string ActiveMode { get; set; }

        [RaisePropertyChanged]
        public virtual int SelectedIndex { get; set; }

        
        [RaisePropertyChanged]
        public virtual BankAccount SpecsBankAccount
        {
            get { return _SpecsBankAccount; }
            set
            {
                _SpecsBankAccount = value;
                if (value != null)
                {
                    this.Code = _SpecsBankAccount.Code;
                    this.Wording = _SpecsBankAccount.Wording;
                    this.Description = _SpecsBankAccount.Description;
                    this.Investment = _SpecsBankAccount.Investment;
                    if(ActiveMode != Modes.ADD)
                        ActiveMode = Modes.MODIFICATION;
                }
            }
        }
        private BankAccount _SpecsBankAccount;


        [RaisePropertyChanged]
        public virtual ObservableCollection<BankAccount> BankAccounts { get; set; }


        
        /// <summary>
        /// COMMANDS
        /// </summary>
        public RelayCommand<int> ValidatedCommand { get; set; }
        public RelayCommand<string> ModeCommand { get; set; }
        public RelayCommand RequestDeleteCommand { get; set; }
    }

}

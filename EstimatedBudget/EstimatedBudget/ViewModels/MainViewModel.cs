using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using EstimatedBudget.POCO;
using System.IO;
using EstimatedBudget.Helpers;
using System.Collections.ObjectModel;
using EstimatedBudget.POCO.Models;
using EstimatedBudget.POCO.DAL;

namespace EstimatedBudget.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///COMMAND
        /// </summary>
        /// ChangeNameFrameCommand

        public RelayCommand<string> EditModeCommand { get; set; }
        public RelayCommand<string> SelectedFrameCommand { get; set; }
        public RelayCommand OpenFlyOutBankAccountCommand { get; set; }


        /// <summary>
        ///CONSTRUCTOR
        /// </summary>
        public MainViewModel()
        {
          //  DriverSqlite.InstallSqLiteDriver();
            //Chek if odbc pilote is Installed
           // if (DriverSqlite.Installed()) { }

            //Check IF Databse Exist
            var PathDatabase = new ConnectionProvider().GetOpenConnection().ConnectionString.Substring(12);
            if (File.Exists(PathDatabase))
            {
                if (new FileInfo(PathDatabase).Length == 0)
                    DataBaseManager.CreateDatabase();
            }
            else
            {
                return;
            }

            PathCurrentFrame = "NeutralView.xaml";
            //NavigationName = NameFrame.BankAccount;
            BankAccounts = new ObservableCollection<BankAccount>(BankAccountDAL.Load());
            OpenFlyOutBankAccountCommand = new RelayCommand(ShowFlyOutBankAccount);
            SelectedFrameCommand = new RelayCommand<string>(SelectedFrame);
            ShowNav = Visibility.Collapsed;
            //Ask Bank Account
            OpenFlyOutBankAccount = true;
        }

        /// <summary>
        /// PROPERTY
        /// </summary>
        /// 

        [RaisePropertyChanged]
        public virtual string DisplayUser { get; set; }

        [RaisePropertyChanged]
        public virtual double ActualWidth { get; set; }

        [RaisePropertyChanged]
        public virtual string PathCurrentFrame { get; set; }

        [RaisePropertyChanged]
        public virtual Visibility ShowNav { get; set; }

        [RaisePropertyChanged]
        public virtual string NavigationName { get; set; }

        [RaisePropertyChanged]
        public virtual bool OpenFlyOutBankAccount { get; set; }

        [RaisePropertyChanged]
        public virtual ObservableCollection<BankAccount> BankAccounts { get; set; }
        [RaisePropertyChanged]
        public virtual BankAccount SelectedBankAccount { get; set; }
        [RaisePropertyChanged]
        public virtual string TitleBankAccount { get; set; }

        [RaisePropertyChanged]
        public virtual string SubTitleBankAccount { get; set; }


        /// <summary>
        /// METHODES
        /// </summary>
        /// 

        private void SelectedFrame(string Path)
        {
            PathCurrentFrame = Path;
            ShowNav = Visibility.Visible;
            NavigationName = NameFrame.Name;
        }

        //FlyOut for Selected BankAccount
        public void ShowFlyOutBankAccount()
        {
            
            if (OpenFlyOutBankAccount)
            {
                OpenFlyOutBankAccount = false;
                //New Selected BankAccount
                Global.BankAccountCode = SelectedBankAccount.Code;
                TitleBankAccount = SelectedBankAccount.Description;
                SubTitleBankAccount = SelectedBankAccount.Code + " " + SelectedBankAccount.Wording;
                string FilterBankAccount = "where B_Code=" + Global.BankAccountCode;
                TransferDAL.Action(RegistrationDAL.Load(FilterBankAccount), TransferDAL.Load(Where: FilterBankAccount));
                //Load Anticipated
                TransferDAL.Action(RegistrationDAL.Load(FilterBankAccount), TransferDAL.Load(Where: FilterBankAccount),true);
                PathCurrentFrame = "NeutralView.xaml";
            }
            else
            {
                OpenFlyOutBankAccount = true;
            }
        }

        //CleanUp
        public override void Cleanup()
        {

            base.Cleanup();
        }
    }
}


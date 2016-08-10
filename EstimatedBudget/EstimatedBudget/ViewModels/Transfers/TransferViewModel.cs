using EstimatedBudget.Helpers;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using EstimatedBudget.POCO.Models;
using EstimatedBudget.POCO.DAL;
using System.ComponentModel;
using System;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;

namespace EstimatedBudget.ViewModels.Transfers
{
    public partial class TransferViewModel : ViewModelBase, IDataErrorInfo
    {
        public TransferViewModel()
        {
            string FilterBankAccount = "where B_Code=" + Global.BankAccountCode;

            //Initialisation
            SelectedIndex = -1;
            ActiveMode = Modes.DEFAULT;
            var LstFrequency = FrequencyDAL.Load();
            var LstCategories = CategoryDAL.Load(FilterBankAccount);
            FrequencyItems = new ObservableCollection<Frequency>(LstFrequency);
            CategoriesItems = new ObservableCollection<Category>(LstCategories);
            BankAccounts = new ObservableCollection<BankAccount>(BankAccountDAL.Load());
            Transfers = new ObservableCollection<Transfer>(TransferDAL.Load(FrequencyItems, CategoriesItems, FilterBankAccount));

            //Commands
            ValidatedCommand = new RelayCommand<int>(Validated);
            ModeCommand = new RelayCommand<string>(ChangeMode);
            RequestDeleteCommand = new RelayCommand(RequestDelete);

            //Find Category Object foreach Transfers
            foreach (var L in Transfers)
                L.Category = LstCategories.Find(o => o.Id == L.C_Id);

            //Find Frequence Object foreach Transfers
            foreach (var L in Transfers)
                L.Frequency = LstFrequency.Find(o => o.Code == L.F_Code);
        }

        /// <summary>
        /// METHODS
        /// </summary>
        public async void Validated(int index)
        {
            if (IsValid)
            {
                if (ActiveMode == Modes.ADD)
                {
                    TransferDAL.Save(SpecsTransfer);
                    Transfers.Add(SpecsTransfer);
                }
                else
                {
                    TransferDAL.Update(SpecsTransfer);
                    Transfers[index] = SpecsTransfer;
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
            {
                SpecsTransfer = new Transfer();
                DateL = DateTime.Now;
                SpecsTransfer = null;
            }
            if (ActiveMode == Modes.DEFAULT)
                SelectedIndex = -1;
        }

        public async void RequestDelete()
        {
            var Result = await DialogService.ShowMessage(DialogService.TitleTextConfirmDelete, DialogService.TextConfirmDelete, MessageDialogStyle.AffirmativeAndNegative);
            if (Result == MessageDialogResult.Affirmative)
            {
                TransferDAL.Delete(SpecsTransfer);
                ActiveMode = Modes.DEFAULT;
                Transfers = new ObservableCollection<Transfer>(TransferDAL.Load());
            }
        }


        /// <summary>
        /// PROPERTIES
        /// </summary>

        //Active ModesRdoBeneficiary
        [RaisePropertyChanged]
        public virtual string ActiveMode { get; set; }

        [RaisePropertyChanged]
        public virtual int SelectedIndex { get; set; }


        [RaisePropertyChanged]
        public virtual Transfer SpecsTransfer
        {
            get { return _SpecsTransfer; }
            set
            {
                _SpecsTransfer = value;
                if (value != null)
                {
                    this.Id = _SpecsTransfer.Id;
                    this.Wording = _SpecsTransfer.Wording;
                    this.Description = _SpecsTransfer.Description;
                    this.DateL = _SpecsTransfer.DateL;
                    this.Price = _SpecsTransfer.Price;
                    this.Way = _SpecsTransfer.Way;
                    if (_SpecsTransfer.B_CodeBeneficiary != 0)
                    {
                        this.B_CodeBeneficiary = _SpecsTransfer.B_CodeBeneficiary;
                        RdoCodeBeneficiary = true;
                    }
                    else
                    {
                        this.Beneficiary = _SpecsTransfer.Beneficiary;
                        RdoBeneficiary = true;
                    }

                    this.SelectedCategory = _SpecsTransfer.Category;
                    this.SelectedFrequency = _SpecsTransfer.Frequency;
                    this.B_Code = Global.BankAccountCode;
                    if (SelectedCategory != null)
                    {
                        this.C_Id = SelectedCategory.Id;
                        this.F_Code = SelectedFrequency.Code;
                    }
                    
                    if (ActiveMode != Modes.ADD)
                        ActiveMode = Modes.MODIFICATION;
                }
            }
        }
        private Transfer _SpecsTransfer;

        [RaisePropertyChanged]
        public virtual bool RdoCodeBeneficiary
        {
            get { return _RdoCodeBeneficiary; }
            set
            {
                _RdoCodeBeneficiary = value;
                if (value == true)
                    Beneficiary = string.Empty;
            }
        }
        private bool _RdoCodeBeneficiary;

        [RaisePropertyChanged]
        public virtual bool RdoBeneficiary { get; set; }

        [RaisePropertyChanged]
        public virtual ObservableCollection<Transfer> Transfers { get; set; }

        [RaisePropertyChanged]
        public virtual ObservableCollection<Frequency> FrequencyItems { get; set; }

        [RaisePropertyChanged]
        public virtual ObservableCollection<Category> CategoriesItems { get; set; }
        [RaisePropertyChanged]
        public virtual ObservableCollection<BankAccount> BankAccounts { get; set; }

        [RaisePropertyChanged]
        public virtual BankAccount SelectedBankAccount { get; set; }


        /// <summary>
        /// COMMANDS
        /// </summary>
        public RelayCommand<int> ValidatedCommand { get; set; }
        public RelayCommand<string> ModeCommand { get; set; }
        public RelayCommand RequestDeleteCommand { get; set; }
    }
}


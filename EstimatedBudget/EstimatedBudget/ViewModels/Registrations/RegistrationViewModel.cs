using EstimatedBudget.Helpers;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using EstimatedBudget.POCO.Models;
using EstimatedBudget.POCO.DAL;
using System.ComponentModel;
using System;
using GalaSoft.MvvmLight.Command;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using EstimatedBudget.Errors;
using System.Collections;
using MahApps.Metro.Controls.Dialogs;

namespace EstimatedBudget.ViewModels.Registrations
{
    public partial class RegistrationViewModel : ViewModelBase, IDataErrorInfo
    {
        List<Category> LstCategories;
        public RegistrationViewModel()
        {
            string FilterBankAccount = "where B_Code=" + Global.BankAccountCode;

            //Initialisation
            SelectedIndex = -1;
            ActiveMode = Modes.DEFAULT;
            //Filter for the Current Month
            SearchSelectedMonth = DateTime.Now.ToString("MM");

            LstCategories = new List<Category>();
            LstCategories = CategoryDAL.Load(FilterBankAccount);
            CategoriesItems = new ObservableCollection<Category>(LstCategories);
            Search();

            //Commands
            ValidatedCommand = new RelayCommand<int>(Validated);
            ModeCommand = new RelayCommand<string>(ChangeMode);
            RequestDeleteCommand = new RelayCommand(RequestDelete);
            SearchCommand = new RelayCommand(Search);
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
                    RegistrationDAL.Save(SpecsRegistration);
                    Registrations.Add(SpecsRegistration);
                }
                else
                {
                    RegistrationDAL.Update(SpecsRegistration);
                    Registrations[index] = SpecsRegistration;
                }
                Search();
                ActiveMode = Modes.DEFAULT;
            }
            else
                await DialogService.ShowMessage2(DialogService.TitleTextErrorValidation, DialogService.TextErrorValidation, MessageDialogStyle.Affirmative);

        }

        public void ChangeMode(string Mode)
        {
            ActiveMode = Mode;
            SelectedIndex = -1;
            if (ActiveMode == Modes.ADD)
            {
                SpecsRegistration = new Registration();
                DateR = DateTime.Now;
            }
        }

        public async void RequestDelete()
        {
            var Result = await DialogService.ShowMessage(DialogService.TitleTextConfirmDelete, DialogService.TextConfirmDelete, MessageDialogStyle.AffirmativeAndNegative);
            if (Result == MessageDialogResult.Affirmative)
            {
                RegistrationDAL.Delete(SpecsRegistration);
                ActiveMode = Modes.DEFAULT;
                Registrations = new ObservableCollection<Registration>(RegistrationDAL.Load());
            }
        }
        public void Search()
        {
            var Month = SearchSelectedMonth;
            string CategoryCode;

            string FilterBankAccount = "where B_Code=" + Global.BankAccountCode;
            string Condition = FilterBankAccount +  " AND strftime('%m', DateR) ='" + Month + "'";

            if (SearchSelectedCategory == null)
            {
                CategoryCode = string.Empty;
            }
            else
            {
                CategoryCode = SearchSelectedCategory.Id.ToString();
                Condition += " AND C_Id=" + CategoryCode;
            } 
            Registrations = new ObservableCollection<Registration>(RegistrationDAL.Load(Condition));

            //Find Category Object foreach Registration
            foreach (var C in Registrations)
                C.Category = LstCategories.Find(o => o.Id == C.C_Id);
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
        public virtual Registration SpecsRegistration
        {
            get { return _SpecsRegistration; }
            set
            {
                _SpecsRegistration = value;
                if (value != null)
                {
                    this.Id = _SpecsRegistration.Id;
                    this.Wording = _SpecsRegistration.Wording;
                    this.Description = _SpecsRegistration.Description;
                    this.DateR = _SpecsRegistration.DateR;
                    this.Price = _SpecsRegistration.Price;
                    this.SelectedCategory = _SpecsRegistration.Category;
                    this.B_Code = Global.BankAccountCode;
                    this.C_Id = _SpecsRegistration.C_Id;
                    this.T_Id = _SpecsRegistration.T_Id;
                    if (ActiveMode != Modes.ADD)
                        ActiveMode = Modes.MODIFICATION;
                }
            }
        }
        private Registration _SpecsRegistration;

        [RaisePropertyChanged]
        public virtual Category SearchSelectedCategory { get; set; }

        [RaisePropertyChanged]
        public virtual string SearchSelectedMonth { get; set; }
        
        [RaisePropertyChanged]
        public virtual ObservableCollection<Registration> Registrations { get; set; }

        [RaisePropertyChanged]
        public virtual ObservableCollection<Category> CategoriesItems { get; set; }

        /// <summary>
        /// COMMANDS
        /// </summary>
        public RelayCommand<int> ValidatedCommand { get; set; }
        public RelayCommand<string> ModeCommand { get; set; }
        public RelayCommand RequestDeleteCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
    }
}

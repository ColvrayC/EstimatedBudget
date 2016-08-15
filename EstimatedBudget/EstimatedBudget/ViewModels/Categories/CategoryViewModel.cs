using EstimatedBudget.Helpers;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using EstimatedBudget.POCO.Models;
using EstimatedBudget.POCO.DAL;
using System;
using GalaSoft.MvvmLight.Command;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;

namespace EstimatedBudget.ViewModels.Categories
{
    public partial class CategoryViewModel : ViewModelBase, IDataErrorInfo
    {
        string FilterBankAccount = "where B_Code=" + Global.BankAccountCode;
        public CategoryViewModel()
        {
            

            //Initialisation
            SelectedIndex = -1;
            ActiveMode = Modes.DEFAULT;
            Categories = new ObservableCollection<Category>(CategoryDAL.Load(FilterBankAccount));

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
            if (IsValid)
            {
                if (ActiveMode == Modes.ADD)
                {
                    CategoryDAL.Save(SpecsCategory);
                    Categories.Add(SpecsCategory);
                }
                else
                {
                    CategoryDAL.Update(SpecsCategory);
                    Categories[index] = SpecsCategory;
                }
                ActiveMode = Modes.DEFAULT;
            }
            else
                await DialogService.ShowMessage(DialogService.TitleTextErrorValidation, DialogService.TextErrorValidation, MessageDialogStyle.Affirmative);

        }

        public void ChangeMode(string Mode)
        {
            ActiveMode = Mode;
            if (ActiveMode == Modes.ADD)
                SpecsCategory = new Category();
                SpecsCategory = null;
            if (ActiveMode == Modes.DEFAULT)
                SelectedIndex = -1;
        }

        public async void RequestDelete()
        {
            var Result = await DialogService.ShowMessage(DialogService.TitleTextConfirmDelete, DialogService.TextConfirmDelete, MessageDialogStyle.AffirmativeAndNegative);
            if (Result == MessageDialogResult.Affirmative)
            {
                CategoryDAL.Delete(SpecsCategory);
                ActiveMode = Modes.DEFAULT;
                Categories = new ObservableCollection<Category>(CategoryDAL.Load(FilterBankAccount));
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
        public virtual Category SpecsCategory
        {
            get { return _SpecsCategory; }
            set
            {
                _SpecsCategory = value;
                if (value != null)
                {
                    this.Id = _SpecsCategory.Id;
                    this.Wording = _SpecsCategory.Wording;
                    this.Targetprice = _SpecsCategory.Targetprice;
                    this.B_Code = Global.BankAccountCode;
                    if (ActiveMode != Modes.ADD)
                        ActiveMode = Modes.MODIFICATION;
                }
            }
        }
        private Category _SpecsCategory;


        [RaisePropertyChanged]
        public virtual ObservableCollection<Category> Categories { get; set; }



        /// <summary>
        /// COMMANDS
        /// </summary>
        public RelayCommand<int> ValidatedCommand { get; set; }
        public RelayCommand<string> ModeCommand { get; set; }
        public RelayCommand RequestDeleteCommand { get; set; }
    }

}

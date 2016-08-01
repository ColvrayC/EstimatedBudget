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

namespace EstimatedBudget.ViewModels.Levies
{
    public partial class LevyViewModel : ViewModelBase, IDataErrorInfo
    {
        public LevyViewModel()
        {
            string FilterBankAccount = "where B_Code=" + Global.BankAccountCode;

            //Initialisation
            SelectedIndex = -1;
            ActiveMode = Modes.DEFAULT;
            var LstFrequency = FrequencyDAL.Load();
            var LstCategories = CategoryDAL.Load(FilterBankAccount);
            FrequencyItems = new ObservableCollection<Frequency>(LstFrequency);
            CategoriesItems = new ObservableCollection<Category>(LstCategories);
            Levies = new ObservableCollection<Levy>(LevyDAL.Load(FrequencyItems, CategoriesItems, FilterBankAccount));

            //Commands
            ValidatedCommand = new RelayCommand<int>(Validated);
            ModeCommand = new RelayCommand<string>(ChangeMode);
            RequestDeleteCommand = new RelayCommand(RequestDelete);

            //Find Category Object foreach Levies
            foreach (var L in Levies)
                L.Category = LstCategories.Find(o => o.Id == L.C_Id);

            //Find Frequence Object foreach Levies
            foreach (var L in Levies)
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
                    LevyDAL.Save(SpecsLevy);
                    Levies.Add(SpecsLevy);
                }
                else
                {
                    LevyDAL.Update(SpecsLevy);
                    Levies[index] = SpecsLevy;
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
                SpecsLevy = new Levy();
                DateL = DateTime.Now;
                SpecsLevy = null;
            }
            if (ActiveMode == Modes.DEFAULT)
                SelectedIndex = -1;
        }

        public async void RequestDelete()
        {
            var Result = await DialogService.ShowMessage(DialogService.TitleTextConfirmDelete, DialogService.TextConfirmDelete, MessageDialogStyle.AffirmativeAndNegative);
            if (Result == MessageDialogResult.Affirmative)
            {
                LevyDAL.Delete(SpecsLevy);
                ActiveMode = Modes.DEFAULT;
                Levies = new ObservableCollection<Levy>(LevyDAL.Load());
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
        public virtual Levy SpecsLevy
        {
            get { return _SpecsLevy; }
            set
            {
                _SpecsLevy = value;
                if (value != null)
                {
                    this.Id = _SpecsLevy.Id;
                    this.Wording = _SpecsLevy.Wording;
                    this.Description = _SpecsLevy.Description;
                    this.DateL = _SpecsLevy.DateL;
                    this.Price = _SpecsLevy.Price;
                    this.SelectedCategory = _SpecsLevy.Category;
                    this.SelectedFrequency = _SpecsLevy.Frequency;
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
        private Levy _SpecsLevy;


        [RaisePropertyChanged]
        public virtual ObservableCollection<Levy> Levies { get; set; }

        [RaisePropertyChanged]
        public virtual ObservableCollection<Frequency> FrequencyItems { get; set; }

        [RaisePropertyChanged]
        public virtual ObservableCollection<Category> CategoriesItems { get; set; }


        /// <summary>
        /// COMMANDS
        /// </summary>
        public RelayCommand<int> ValidatedCommand { get; set; }
        public RelayCommand<string> ModeCommand { get; set; }
        public RelayCommand RequestDeleteCommand { get; set; }
    }
}


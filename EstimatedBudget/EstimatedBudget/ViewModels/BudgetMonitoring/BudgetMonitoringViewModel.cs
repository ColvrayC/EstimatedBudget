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
using EstimatedBudget.POCO.Models;

namespace EstimatedBudget.ViewModels.BudgetMonitoring
{
    public class BudgetMonitoringViewModel : ViewModelBase
    {
        string FilterBankAccount = "where B_Code=" + Global.BankAccountCode;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public BudgetMonitoringViewModel()
        {
            NormalDisplay = true;
            LstBudgetMonitoring = new ObservableCollection<BusinessBudgetMonitoring>(BudgetMonitoringDAL.Load(FilterBankAccount, DateTime.Now));
            DisplayDate = DateTime.Now.ToString("MMMM yyyy").ToUpper();
            NormalDisplay = true;

            //Commands Init
            PreviousMonthCommand = new RelayCommand(PreviousMonth);
            NextMonthCommand = new RelayCommand(NextMonth);
        }

        /// <summary>
        /// METHODS
        /// </summary>
        public void PreviousMonth()
        {
           var Date = DateTime.Parse(DisplayDate).AddMonths(-1);
           LstBudgetMonitoring = new ObservableCollection<BusinessBudgetMonitoring>(BudgetMonitoringDAL.Load(FilterBankAccount, Date));
           DisplayDate = Date.ToString("MMMM yyyy").ToUpper();
        }

        public void NextMonth()
        {
            var Date = DateTime.Parse(DisplayDate).AddMonths(1);
            LstBudgetMonitoring = new ObservableCollection<BusinessBudgetMonitoring>(BudgetMonitoringDAL.Load(FilterBankAccount, Date));
            DisplayDate = Date.ToString("MMMM yyyy").ToUpper();
        }


        /// <summary>
        /// COMMANDS
        /// </summary>
        [RaisePropertyChanged]
        public RelayCommand PreviousMonthCommand { get; set; }
        [RaisePropertyChanged]
        public RelayCommand NextMonthCommand { get; set; }

        /// <summary>
        /// PROPERTIES
        /// </summary>
        [RaisePropertyChanged]
        public virtual ObservableCollection<BusinessBudgetMonitoring> LstBudgetMonitoring { get; set; }

        [RaisePropertyChanged]
        public bool NormalDisplay { get; set; }

        [RaisePropertyChanged]
        public bool AnticipateDisplay { get; set; }

        [RaisePropertyChanged]
        public virtual string DisplayDate { get; set; }

        public void Load()
        {
            
        }
    }

   
}

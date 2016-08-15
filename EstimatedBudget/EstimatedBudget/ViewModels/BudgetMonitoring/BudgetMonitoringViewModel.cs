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
using System.Linq;

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
            DisplayDate = DateTime.Now.ToString("MMMM yyyy").ToUpper();
            NormalDisplay = true;

            //TotalMoney
            foreach(var R in RegistrationDAL.Load(FilterBankAccount))
            {
                if (!R.Way)
                    TotalMoney -= R.Price;
                else
                    TotalMoney += R.Price;
            }
            PreviousMonthCommand = new RelayCommand(PreviousMonth);
            NextMonthCommand = new RelayCommand(NextMonth);
        }

        /// <summary>
        /// METHODS
        /// </summary>
        public void PreviousMonth()
        {
           var Date = DateTime.Parse(DisplayDate).AddMonths(-1);
            if (AnticipateDisplay)
                LstBudgetMonitoring = Anticipated(Date);
            else
                LstBudgetMonitoring = new ObservableCollection<BusinessBudgetMonitoring>(BudgetMonitoringDAL.Load(FilterBankAccount, Date));

            DisplayDate = Date.ToString("MMMM yyyy").ToUpper();
        }

        public void NextMonth()
        {
            var Date = DateTime.Parse(DisplayDate).AddMonths(1);
            if (AnticipateDisplay)
                LstBudgetMonitoring = Anticipated(Date);
            else
                LstBudgetMonitoring = new ObservableCollection<BusinessBudgetMonitoring>(BudgetMonitoringDAL.Load(FilterBankAccount, Date));
            DisplayDate = Date.ToString("MMMM yyyy").ToUpper();
        }

        public ObservableCollection<BusinessBudgetMonitoring> Anticipated(DateTime Date)
        {
            return new ObservableCollection<BusinessBudgetMonitoring>(BudgetMonitoringDAL.Load(FilterBankAccount, Date,true));
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
        public bool NormalDisplay {
            get { return _NormalDisplay; }
            set {
                _NormalDisplay = value;
                var Date = DateTime.Parse(DisplayDate);
                if (value)
                    LstBudgetMonitoring = new ObservableCollection<BusinessBudgetMonitoring>(BudgetMonitoringDAL.Load(FilterBankAccount, Date));
            }
        }
        private bool _NormalDisplay;

        [RaisePropertyChanged]
        public bool AnticipateDisplay
        {
            get { return _AnticipateDisplay; }
            set {
                _AnticipateDisplay = value;
                var Date =DateTime.Parse(DisplayDate);
                if(value)
                    LstBudgetMonitoring = Anticipated(Date);
                }
        }
        private bool _AnticipateDisplay;
        [RaisePropertyChanged]
        public virtual string DisplayDate { get; set; }

        [RaisePropertyChanged]
        public virtual decimal TotalMoney { get; set; }

        public void Load()
        {
            
        }
    }

   
}

using EstimatedBudget.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EstimatedBudget.Converters
{
    public class FrameConverter : IValueConverter
    {
        const string BudgetMonitoring = "BudgetMonitoringView.xaml";
        const string BankAccount = "BankAccountView.xaml";
        const string Category = "CategoryView.xaml";
        const string Transfer = "TransferView.xaml";
        const string Registration = "RegistrationView.xaml";
        const string Neutral = "NeutralView.xaml";


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Result = "";
            if (value == null)
                value = Neutral;

            switch (value.ToString())
            {
                case BudgetMonitoring:
                    Result = BudgetMonitoring;
                    NameFrame.Name = NameFrame.BudgetMonitoring;
                    break;
                case BankAccount:
                    Result = BankAccount;
                    NameFrame.Name = NameFrame.BankAccount;
                    break;
                case Category:
                    Result = Category;
                    NameFrame.Name = NameFrame.Category;
                    break;
                case Transfer:
                    Result = Transfer;
                    NameFrame.Name = NameFrame.Transfer;
                    break;
                case Registration:
                    Result = Registration;
                    NameFrame.Name = NameFrame.Registration;
                    break;
                case Neutral:
                    Result = Neutral;
                    NameFrame.Name = string.Empty; ;
                    break;
            }
            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}


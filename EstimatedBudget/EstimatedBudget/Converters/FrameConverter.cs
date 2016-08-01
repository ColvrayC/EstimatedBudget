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
        const string Levy = "LevyView.xaml";
        const string Registration = "RegistrationView.xaml";



        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Result = "";
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
                case Levy:
                    Result = Levy;
                    NameFrame.Name = NameFrame.Levy;
                    break;
                case Registration:
                    Result = Registration;
                    NameFrame.Name = NameFrame.Registration;
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


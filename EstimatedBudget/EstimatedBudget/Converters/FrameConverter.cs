using System;
using System.Globalization;
using System.Windows.Data;

namespace EstimatedBudget.Converters
{
    public class FrameConverter : IValueConverter
    {
        //Compta Matière
        const string BankAccount = "BankAccountView.xaml";
        const string Category = "CategoryView.xaml";
        const string Levy = "LevyView.xaml";
        const string Mode = "ModeView.xaml";
        const string Registration = "RegistrationView.xaml";



        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Result = "";
            switch (value.ToString())
            {
                case BankAccount:
                    Result = BankAccount;
                    break;
                case Category:
                    Result = Category;
                    break;
                case Levy:
                    Result = Levy;
                    break;
                case Mode:
                    Result = Mode;
                    break;
                case Registration:
                    Result = Registration;
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


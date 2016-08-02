using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace EstimatedBudget.Converters
{
    public class VisiblityToCheckedConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myValue = (bool)value;
            var myParameter = (string)parameter;

            //Si c'est le Yes 
            if (myValue == true && myParameter == "1")
                return Visibility.Visible;

            if (myValue == false && myParameter == "2")
                return Visibility.Visible;

            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myValue = (bool)value;
            var myParameter = (string)parameter;

            if (myValue == true && myParameter == "1")
                return Visibility.Visible;

            if (myValue == false && myParameter == "2")
                return Visibility.Visible;

            return Visibility.Collapsed;
        }
    }
}


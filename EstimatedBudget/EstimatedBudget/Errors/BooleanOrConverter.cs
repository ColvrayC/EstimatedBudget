using System;
using System.Windows.Data;

namespace EstimatedBudget.Errors
{
    public class BooleanOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {



            // return values.OfType<bool>().Any(value => value);
            //  }
            foreach (object value in values)
            {
                if (value is bool)
                {
                    if ((bool)value == true)
                        return true;
                }
                else
                    return false;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

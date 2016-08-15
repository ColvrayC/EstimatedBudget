using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace EstimatedBudget.Converters
{
   class MoneyToNegativeConverter : IMultiValueConverter
    { // converter pour parametrage
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var Money = (decimal)values[0];
            var Way = (bool)values[1];

            if ((bool)!Way)
                Money = Money * -1;

            return Money.ToString("0.00 €");
        }

      
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EstimatedBudget.Converters
{
    class CheckBoxConverter : IValueConverter
    { 
      /// Affiche Champs Libre Si la propriété est != Null
      /// </summary>
      /// <param name="Mode envoyé depuis le XAML"></param>
      /// <param name="targetType"></param>
      /// <param name="parameter"></param>
      /// <param name="culture"></param>
      /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myValue = (bool)value;
            var myParameter = (string)parameter;

            //Si c'est le Yes 
            if (myValue == true && myParameter == "1")
                return true;

            if (myValue == false && myParameter == "2")
                return true;

            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myValue = (bool)value;
            var myParameter = (string)parameter;
             
            if (myValue == true && myParameter == "1")
                return true;

            if (myValue == false && myParameter == "2")
                return true;

            return false;
        }
    }
}

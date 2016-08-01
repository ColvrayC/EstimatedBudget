using System;
using System.Globalization;
using System.Windows.Data;

namespace EstimatedBudget.Converters
{
    class CheckBoxToYesNoConverter : IValueConverter
    { // converter pour parametrage
        /// <summary>
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
            if (myValue == true && myParameter == "Yes")
                return true;

            if (myValue == false && myParameter == "No")
                return true;

            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myValue = (bool)value;
            var myParameter = (string)parameter;
            //Si c'est le Yes 
            if (myValue == true && myParameter == "Yes")
                return true;

            if (myValue == false && myParameter == "No")
                return true;

            return false;
        }
    }
}

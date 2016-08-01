using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EstimatedBudget.Converters
{
    class DisplayLevyConverter : IValueConverter
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
            if (value != null)
                return " (Prélèvement)";
            else
                return string.Empty;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


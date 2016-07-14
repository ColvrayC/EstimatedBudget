using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EstimatedBudget.Converters
{
    //Change Column Auto or Star for a good adaptive Datagrid, when Specs is Open
    public class AdaptiveDataGridConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var NewSizeDataGrid = System.Convert.ToInt32(values[0]);
            var SizeFiche = System.Convert.ToInt32(values[1]);
            var SizeFrame = System.Convert.ToInt32(values[2]);
            var FicheVisibility = values[3];

            if (SizeFrame < NewSizeDataGrid + SizeFiche + 50 && (Visibility)FicheVisibility == Visibility.Visible)
                return new GridLength(1, GridUnitType.Star);

            //Si datagrid aussi grand que frame
            if (SizeFrame <= NewSizeDataGrid + 50 && (Visibility)FicheVisibility == Visibility.Collapsed)
                return new GridLength(1, GridUnitType.Star);


            return GridLength.Auto;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


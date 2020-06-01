using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebAPI.Models.APIModels;
using Xamarin.Forms;

namespace FamilyFinance.Converters
{
    public class ListSizeToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                var list = value as List<Purse>;
                return list.Count == 0 ? false : true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}

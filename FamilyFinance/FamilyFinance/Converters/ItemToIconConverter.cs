using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WebAPI.Models.APIModels;
using Xamarin.Forms;

namespace FamilyFinance.Converters
{
    public class ItemToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Report)
            {
                Report item = (Report)value;
                return item.PersonId == null ? "family_icon" : "user_icon";
            }
            else if(value is Purpose)
            {
                Purpose item = (Purpose)value;
                return item.PersonId == null ? "family_icon" : "user_icon";
            }
            else if (value is Purse)
            {
                Purse item = (Purse)value;
                return item.PersonId == null ? "family_icon" : "user_icon";
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return default;
        }
    }
}

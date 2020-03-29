using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FamilyFinance.Converters
{
    class PurposeProgressToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color progressColor = new Color();
            double currentValue = (double)value;
            if(currentValue == 1)
            {
                progressColor = Color.Green;
            }
            else if(currentValue >= 0.3)
            {
                progressColor = Color.Orange;
            }
            else
            {
                progressColor = Color.Red;
            }

            return progressColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return default;             
        }
    }
}

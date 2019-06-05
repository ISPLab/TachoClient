using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace TachoServiceClient.Views
{
    public class DateTimeToColorConverter : IValueConverter
    {
        //Show Red if now - value > parameter (parameter min)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int p = 0;
            if (!String.IsNullOrEmpty((string)parameter))
                p = System.Convert.ToInt16((string)parameter);
            DateTime date = (DateTime)value;

            if (date > DateTime.Now) return Color.Yellow;

            return date < DateTime.Now.Subtract(new TimeSpan(0, p, 0)) ? Color.Red : Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}

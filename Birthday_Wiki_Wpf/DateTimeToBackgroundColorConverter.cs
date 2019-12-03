using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;

namespace Birthday_Wiki_Wpf
{
    [ValueConversion(typeof(DateTime), typeof(Brush))]
    public class DateTimeToBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime)value;
            if (dateTime.Date.Day == DateTime.Now.Day && dateTime.Date.Month == DateTime.Now.Month)
                return Brushes.LightPink;
            if (dateTime.Date.AddDays(-1).Day == DateTime.Now.Day && dateTime.Date.AddDays(-1).Month == DateTime.Now.Month)
                return Brushes.LightCyan;
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

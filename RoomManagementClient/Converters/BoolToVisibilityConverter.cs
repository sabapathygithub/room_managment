using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RoomManagementClient.Converters
{
    /// <summary>
    /// Converts the boolean value to Visibility object and vice versa.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val && val)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility val && val == Visibility.Visible)
            {
                return true;
            }
            return false;
        }
    }
}

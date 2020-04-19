using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GitHubUsers.Converters
{
    public class StarColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int starCount)
                return starCount > 0 ? Application.Current.Resources["AccentColor"] : Application.Current.Resources["TextColorLight"];

            return Application.Current.Resources["TextColorLight"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using Xamarin.Forms;

namespace GitHubUsers.Converters
{
    public class LanguageColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string languageName)
            {
                if (Utilities.GithubLanguageColors.ColorCodes.TryGetValue(languageName, out string languageColorCode))
                    return languageColorCode;
                return Application.Current.Resources["TextColorLight"];
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

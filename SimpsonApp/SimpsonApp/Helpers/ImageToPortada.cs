using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SimpsonApp.Helpers
{
    public class ImageToPortada : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            string image = value.ToString().Remove(0,9);
            
            string path=  System.Environment.GetFolderPath(Environment.SpecialFolder.Personal)+  $"{image}";
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

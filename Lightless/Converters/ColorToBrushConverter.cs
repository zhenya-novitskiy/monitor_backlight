using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Lightless.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var col = (Pixel)value;
            var c = Color.FromArgb(255, col.R, col.G, col.B);
            return new SolidColorBrush(c);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var c = (SolidColorBrush)value;
            var col = new Pixel() {R = c.Color.R, G = c.Color.G, B = c.Color.B};
            return col;
        }
    }
}

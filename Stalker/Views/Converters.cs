using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Stalker.Views;

public class BoolToAccentBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is true
            ? new SolidColorBrush(Color.FromRgb(99, 102, 241))
            : new SolidColorBrush(Color.FromRgb(152, 152, 176));

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

public class BoolToGoldBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is true
            ? new SolidColorBrush(Color.FromRgb(234, 179, 8))
            : new SolidColorBrush(Color.FromRgb(152, 152, 176));

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

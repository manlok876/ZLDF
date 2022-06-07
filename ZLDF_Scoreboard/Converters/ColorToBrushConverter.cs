using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ZLDF.Scoreboard.Converters
{
	internal class ColorToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return null;
			}

			if (value is Color)
			{
				Color color = (Color)value;
				return new SolidColorBrush(color);
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush? brush = (SolidColorBrush) value;
			if (brush == null)
			{
				return Colors.Transparent;
			}
			return brush.Color;
		}
	}
}

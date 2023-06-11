using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ZLDF.WPF.Converters
{
	public class BoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Visibility.Visible;
			}

			if (value is bool)
			{
				bool shouldBeVisible = (bool)value;
				if (shouldBeVisible)
				{
					return Visibility.Visible;
				}

				if (parameter is bool)
				{
					bool shouldBeCollapsed = (bool)parameter;
					return shouldBeCollapsed ? Visibility.Collapsed : Visibility.Hidden;
				}
				return Visibility.Hidden;
			}

			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Visibility? visibility = (Visibility)value;
			if (visibility != null)
			{
				if (visibility == Visibility.Visible)
				{
					return true;
				}
			}
			return false;
		}
	}
}

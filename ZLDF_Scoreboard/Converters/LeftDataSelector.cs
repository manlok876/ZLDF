using System;
using System.Globalization;
using System.Windows.Data;

namespace ZLDF.Scoreboard.Converters
{
	internal class LeftDataSelector : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			object? result = null;

			if (values.Length >= 1)
			{
				result = values[0];
			}

			if (values.Length > 2 && (bool) values[2])
			{
				result = values[1];
			}

			return System.Convert.ChangeType(result, targetType);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

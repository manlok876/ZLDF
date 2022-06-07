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

			if (values.Length > 2 && values[2] is bool)
			{
				bool bIsFlipped = (bool) values[2];
				if (bIsFlipped)
				{
					result = values[1];
				}
			}

			if (result is IConvertible)
			{
				return System.Convert.ChangeType(result, targetType);
			}

			return result;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

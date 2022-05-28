using System;
using System.Globalization;
using System.Windows.Data;

namespace ZLDF.Scoreboard.Converters
{
	internal class LeftDataSelector : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.Length < 1)
			{
				return "";
			}

			if (values.Length <= 2)
			{
				return values[0].ToString();
			}

			if (values.Length > 2 && (bool) values[2])
			{
				return values[1].ToString();
			}
			else
			{
				return values[0].ToString();
			}
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

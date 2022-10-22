using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ZLDF.Classes;

namespace ZLDF.Scoreboard.Converters
{
	internal class FighterToNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "";
			}

			if (value is Fighter)
			{
				Fighter fighter = (Fighter)value;

				if (parameter is bool)
				{
					bool shouldUseFullName = (bool)parameter;
					if (shouldUseFullName)
					{
						return $"{fighter.LastName} {fighter.FirstName}";
					}
					else
					{
						return $"{fighter.LastName} {fighter.FirstName.Substring(0, 1)}.";
					}
				}

				return $"{fighter.LastName} {fighter.FirstName.Substring(0, 1)}.";
			}

			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}

using System;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using ZLDF.Core;

namespace ZLDF.WPF.Converters
{
	public class PersonToNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "";
			}

			// TODO: implement proper formatting
			bool shouldUseFullName = false;
			if (parameter is bool)
			{
				shouldUseFullName = (bool) parameter;
			}

			if (value is Person person)
			{
				return GetNameStringForPerson(person, shouldUseFullName);
			}
			else if (value is IRole role)
			{
				if (role.Person != null)
				{
					return GetNameStringForPerson(role.Person, shouldUseFullName);
				}
				else
				{
					return "<Unassigned>";
				}
			}

			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		public string GetNameStringForPerson(Person person, bool shouldUseFullName)
		{
			if (shouldUseFullName)
			{
				return $"{person.LastName} {person.FirstName}";
			}
			else
			{
				return $"{person.LastName} {person.FirstName.Substring(0, 1)}.";
			}
		}


	}
}

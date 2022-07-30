using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using ZLDF.Classes;

namespace ZLDF.Scoreboard.Converters
{
	internal class EventStateToBrushConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return new SolidColorBrush(Colors.Transparent);
			}

			if (value is EventState)
			{
				EventState eventState = (EventState)value;
				Color color = Colors.Transparent;
				switch (eventState)
				{
					case EventState.NotStarted:
						color = Colors.LightGray;
						break;
					case EventState.Scheduled:
						color = Colors.LightBlue;
						break;
					case EventState.Cancelled:
						color = Colors.DarkGray;
						break;
					case EventState.InProgress:
						color = Colors.LightGreen;
						break;
					case EventState.Paused:
						color = Colors.LightYellow;
						break;
					case EventState.Aborted:
						color = Colors.Red;
						break;
					case EventState.Finished:
						color = Colors.Green;
						break;
				}
				return new SolidColorBrush(color);
			}

			return new SolidColorBrush(Colors.Transparent);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return EventState.Unknown;
		}
	}
}

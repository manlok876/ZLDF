using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public enum EventState
	{
		Unknown,
		NotStarted,
		Scheduled,
		InProgress,
		Paused,
		Finished,
		Cancelled
	}

	public abstract class Event : BindableBase
	{
		private EventState _state;
		public EventState State
		{
			get
			{
				return _state;
			}
			set
			{
				SetProperty(ref _state, value);
			}
		}

		protected Event()
		{
			_state = EventState.Unknown;
		}
	}
}

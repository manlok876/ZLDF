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
		private Guid _id;
		private EventState _state;

		/// <summary>
		/// Id property is used for persistent tracking of events, especially between different runs
		/// </summary>
		public Guid Id
		{
			get { return _id; }
			private set
			{
				SetProperty(ref _id, value);
			}
		}

		public EventState State
		{
			get { return _state; }
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

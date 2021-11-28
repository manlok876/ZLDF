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
		Cancelled,
		InProgress,
		Paused,
		Aborted,
		Finished
	}

	public abstract class Event : BindableBase
	{
		private Guid _id;

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

		public virtual EventState State
		{
			get { return EventState.Unknown; }
			set { }
		}

		public bool HasStarted
		{
			get
			{
				return State == EventState.InProgress ||
					State == EventState.Paused ||
					State == EventState.Aborted ||
					State == EventState.Finished;
			}
		}

		public bool WaitingToStart
		{
			get
			{
				return State == EventState.NotStarted ||
					State == EventState.Scheduled ||
					State == EventState.Paused;
			}
		}

		public bool IsOver
		{
			get
			{
				return State == EventState.Cancelled ||
					State == EventState.Aborted ||
					State == EventState.Finished;
			}
		}

		protected Event()
		{
		}
	}
}

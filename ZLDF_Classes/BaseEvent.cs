using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public abstract class BaseEvent : BindableBase, IEvent
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
			get
			{
				return EventState.Unknown;
			}
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

		public bool IsOverSuccessfully
		{
			get { return State == EventState.Finished; }
		}

		protected BaseEvent()
		{

		}
	}
}

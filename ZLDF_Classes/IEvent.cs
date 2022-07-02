using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

	internal interface IEvent
	{
		public abstract EventState State
		{
			get;
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
	}
}

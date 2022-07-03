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

	public interface IEvent
	{
		public abstract EventState State
		{
			get;
		}
	}
}

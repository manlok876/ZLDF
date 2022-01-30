using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ZLDF.Classes.Matchmaking
{
	public abstract class MatchmakingBase : BindableBase
	{
		public Guid Id { get; private set; }

		public virtual bool HasFinished
		{
			get;
		}

		public virtual bool ReadyToContinue
		{
			get;
		}

		public virtual bool IsWaitingForTours
		{
			get;
		}

		public virtual bool IsFinished
		{
			get;
		}

		public virtual Tour EndedTours
		{
			get;
		}

		public MatchmakingBase()
		{
			Id = Guid.NewGuid();
		}
	}
}

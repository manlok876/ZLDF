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

		public MatchmakingBase()
		{
			Id = Guid.NewGuid();
		}
	}
}

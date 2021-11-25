using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public abstract class Fight : Event
	{
		public abstract ImmutableArray<Fighter> Fighters { get; }
		public abstract float GetFighterScore(Fighter fighter);

		protected Fight()
		{
		}
	}
}

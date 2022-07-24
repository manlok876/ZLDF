using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public abstract class Fight : BaseEvent
	{
		public abstract IEnumerable<Fighter> Fighters { get; }
		public abstract float GetFighterScore(Fighter? fighter);
		public abstract void AddFighterScore(Fighter? fighter, float deltaScore);

		protected Fight()
		{
		}
	}
}

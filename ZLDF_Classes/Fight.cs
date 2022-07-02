using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public abstract class Fight : BaseEvent
	{
		private EventState _fightState = EventState.Unknown;

		public override EventState State
		{
			get { return _fightState; }
		}
		public abstract IEnumerable<Fighter> Fighters { get; }
		public abstract float GetFighterScore(Fighter? fighter);
		public abstract void AddFighterScore(Fighter? fighter, float deltaScore);

		protected Fight()
		{
		}
	}
}

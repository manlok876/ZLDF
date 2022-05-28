using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public abstract class Fight : Event
	{
		private EventState _fightState = EventState.Unknown;

		public override EventState State
		{
			get { return _fightState; }
			set
			{
				SetProperty(ref _fightState, value);
			}
		}
		public abstract IEnumerable<Fighter> Fighters { get; }
		public abstract float GetFighterScore(Fighter? fighter);

		protected Fight()
		{
		}
	}
}

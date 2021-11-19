using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public abstract class Nomination : Event
	{
		// Participants
		public abstract ImmutableArray<Fighter> Fighters { get; }
		// Fight rules

		// Matchmaking

		public abstract ImmutableArray<Fight> Fights { get; }


		// Judges?
	}
}

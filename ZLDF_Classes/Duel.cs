using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ZLDF.Classes
{
	public class Duel : Fight
	{
		/// <summary>
		/// First fighter in a duel. Immutable, if pairings changed - create new fights
		/// </summary>
		public Fighter FirstFighter
		{
			get; set;
		}

		/// <summary>
		/// Second fighter in a duel. Immutable, if pairings changed - create new fights
		/// </summary>
		public Fighter SecondFighter
		{
			get; set;
		}

		public override ImmutableArray<Fighter> Fighters
		{
			get
			{
				return new ImmutableArray<Fighter> { FirstFighter, SecondFighter };
			}
		}

		public float FirstFighterScore
		{
			get; set;
		}
		public float SecondFighterScore
		{
			get; set;
		}
		public override float GetFighterScore(Fighter fighter)
		{
			if (fighter == FirstFighter)
			{
				return FirstFighterScore;
			}
			else if (fighter == SecondFighter)
			{
				return SecondFighterScore;
			}
			else
			{
				return 0.0f;
			}
		}

		public Duel()
		{
		}

		public void Init(Fighter firstFighter, Fighter secondFighter)
		{
			FirstFighter = firstFighter;
			SecondFighter = secondFighter;
		}
	}
}

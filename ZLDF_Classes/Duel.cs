using System;
using System.Collections.Generic;
using System.Text;

namespace ZLDF.Classes
{
	public class Duel : Fight
	{
		public Fighter FighterOne
		{
			get; private set;
		}
		public Fighter FighterTwo
		{
			get; private set;
		}

		public override Fighter[] Fighters
		{
			get
			{
				return new Fighter[2] { FighterOne, FighterTwo };
			}
		}

		public float FighterOneScore
		{
			get; private set;
		}
		public float FighterTwoScore
		{
			get; private set;
		}
		public override float GetFighterScore(Fighter TheFighter)
		{
			if (TheFighter == FighterOne)
			{
				return FighterOneScore;
			}
			else if (TheFighter == FighterTwo)
			{
				return FighterTwoScore;
			}
			else
			{
				return 0.0f;
			}
		}

		Duel(Fighter FirstFighter, Fighter SecondFighter)
		{
			FighterOne = FirstFighter;
			FighterTwo = SecondFighter;
		}
	}
}

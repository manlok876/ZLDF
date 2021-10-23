using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		Duel()
		{
			FighterOne = null;
			FighterTwo = null;
		}

		Duel(Fighter FirstFighter, Fighter SecondFighter)
		{
			FighterOne = FirstFighter;
			FighterTwo = SecondFighter;
		}
	}
}

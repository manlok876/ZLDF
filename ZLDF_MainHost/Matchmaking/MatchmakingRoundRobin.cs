using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Classes;

namespace ZLDF.MainHost.Matchmaking
{
	internal class MatchmakingRoundRobin : MatchmakingBase
	{
		public static List<Fight> GetFightsFor(IEnumerable<Fighter> AllFighters)
		{
			Fighter[] FightersArray = AllFighters.ToArray();

			int FightersCount = FightersArray.Length;
			if (FightersCount < 2)
			{
				return new List<Fight>();
			}

			int FightsCount = FightersCount * (FightersCount - 1) / 2;
			List<Fight> Result = new List<Fight>(FightsCount);

			// Idea is to repeat going around the group in one direction with given step
			// until every fighter is used as first in a duel
			HashSet<Fighter> UsedFighters = new HashSet<Fighter>();

			// Ideally, we don't want 2 fights in a row for any fighter
			Duel? PrevDuel = null;
			bool FighterWasInPrevDuel(Fighter CurrFighter)
			{
				if (PrevDuel == null)
				{
					return false;
				}
				return PrevDuel.FighterOne == CurrFighter ||
					PrevDuel.FighterTwo == CurrFighter;
			}

			for (int Step = 1; Step <= FightersCount / 2; Step++)
			{
				int FirstFighterIdx = 0;
				if (FighterWasInPrevDuel(FightersArray[FirstFighterIdx]))
				{
					FirstFighterIdx++;
				}
				int SecondFighterIdx = (FirstFighterIdx + Step) % FightersCount;

				while (UsedFighters.Count < FightersCount)
				{
					Fighter FirstFighter = FightersArray[FirstFighterIdx];
					Fighter SecondFighter = FightersArray[SecondFighterIdx];

					Duel NewDuel = new Duel(FirstFighter, SecondFighter);
					Result.Add(NewDuel);
					PrevDuel = NewDuel;

					UsedFighters.Add(FirstFighter);
					// For even N, X + N/2 + N/2 = X (mod N), => we are marking both fighters "used" at this step
					if (Step * 2 == FightersCount)
					{
						UsedFighters.Add(SecondFighter);
					}

					// Compute next indices
					FirstFighterIdx = (SecondFighterIdx + 1) % FightersCount;
					SecondFighterIdx = (FirstFighterIdx + Step) % FightersCount;
					// TODO: test
					if (UsedFighters.Contains(FightersArray[FirstFighterIdx]) ||
						FighterWasInPrevDuel(FightersArray[SecondFighterIdx]))
					{
						FirstFighterIdx++;
					}
					FirstFighterIdx = FirstFighterIdx % FightersCount;
					SecondFighterIdx = (FirstFighterIdx + Step) % FightersCount;
				}
				UsedFighters.Clear();
			}

			return Result;
		}
	}
}

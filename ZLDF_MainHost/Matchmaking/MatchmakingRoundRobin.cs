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
		public static Fight[] GetFightsFor(Fighter[] AllFighters)
		{
			int FightersCount = AllFighters.Length;
			if (FightersCount < 2)
			{
				return new Fight[0];
			}
			int FightsCount = FightersCount * (FightersCount - 1) / 2;
			Fight[] Result = new Fight[FightsCount];

			int NextFightIdx = 0;
			// Idea is to repeat going around the group in one direction with given step
			// until every fighter is used as first in a duel
			HashSet<Fighter> UsedFighters = new HashSet<Fighter>();

			bool FighterWasInPrevDuel(Fighter CurrFighter)
			{
				if (NextFightIdx < 1)
				{
					return false;
				}
				Duel? PrevDuel = Result[NextFightIdx - 1] as Duel;
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
				if (FighterWasInPrevDuel(AllFighters[FirstFighterIdx]))
				{
					FirstFighterIdx++;
				}
				int SecondFighterIdx = (FirstFighterIdx + Step) % FightersCount;

				while (UsedFighters.Count < FightersCount)
				{
					Fighter FirstFighter = AllFighters[FirstFighterIdx];
					Fighter SecondFighter = AllFighters[SecondFighterIdx];

					Result[NextFightIdx] = new Duel(FirstFighter, SecondFighter);
					NextFightIdx++;

					UsedFighters.Add(FirstFighter);
					if (Step * 2 == FightersCount)
					{
						UsedFighters.Add(SecondFighter);
					}

					// Compute next indices
					FirstFighterIdx = (SecondFighterIdx + 1) % FightersCount;
					SecondFighterIdx = (FirstFighterIdx + Step) % FightersCount;
					if (UsedFighters.Contains(AllFighters[FirstFighterIdx]) ||
						FighterWasInPrevDuel(AllFighters[SecondFighterIdx]))
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

/*
 * 2: 0-1
 * 3: 0-1 1-2 0-2
 * 4: 0-1 2-3 1-2 1-3 0-2 0-3
 * 5: 0-1 2-3 0-4 1-2 1-3 0-2 0-3
 *    0-4 1-4 2-4 3-4
 */
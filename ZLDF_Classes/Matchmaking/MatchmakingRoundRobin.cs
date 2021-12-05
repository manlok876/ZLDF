using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes.Matchmaking
{
	public class MatchmakingRoundRobin : MatchmakingBase
	{
		public static List<Fight> GetFightsFor(IEnumerable<Fighter> AllFighters)
		{
			Fighter[] fightersArray = AllFighters.ToArray();

			int fightersCount = fightersArray.Length;
			if (fightersCount < 2)
			{
				return new List<Fight>();
			}

			int fightsCount = fightersCount * (fightersCount - 1) / 2;
			List<Fight> result = new List<Fight>(fightsCount);

			// Idea is to repeat going around the group in one direction with given step
			// until every fighter is used as first in a duel
			HashSet<Fighter> usedFighters = new HashSet<Fighter>();

			// Ideally, we don't want 2 fights in a row for any fighter
			Duel? prevDuel = null;
			bool FighterWasInPrevDuel(Fighter currFighter)
			{
				if (prevDuel == null)
				{
					return false;
				}
				return prevDuel.FirstFighter == currFighter ||
					prevDuel.SecondFighter == currFighter;
			}

			for (int step = 1; step <= fightersCount / 2; step++)
			{
				int firstFighterIdx = 0;
				if (FighterWasInPrevDuel(fightersArray[firstFighterIdx]))
				{
					firstFighterIdx++;
				}
				int secondFighterIdx = (firstFighterIdx + step) % fightersCount;

				while (usedFighters.Count < fightersCount)
				{
					while (usedFighters.Contains(fightersArray[firstFighterIdx]) ||
						FighterWasInPrevDuel(fightersArray[secondFighterIdx]))
					{
						firstFighterIdx = (firstFighterIdx + 1) % fightersCount;
						secondFighterIdx = (firstFighterIdx + step) % fightersCount;
					}
					Fighter firstFighter = fightersArray[firstFighterIdx];
					Fighter secondFighter = fightersArray[secondFighterIdx];

					Duel newDuel = new Duel();
					newDuel.Init(firstFighter, secondFighter);
					result.Add(newDuel);
					prevDuel = newDuel;

					usedFighters.Add(firstFighter);
					// For even N, X + N/2 + N/2 = X (mod N), => we are marking both fighters "used" at this step
					if (step * 2 == fightersCount)
					{
						usedFighters.Add(secondFighter);
					}

					// Compute next indices
					firstFighterIdx = (secondFighterIdx + 1) % fightersCount;
					secondFighterIdx = (firstFighterIdx + step) % fightersCount;
				}
				usedFighters.Clear();
			}

			return result;
		}
	}
}

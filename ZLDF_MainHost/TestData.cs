using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Classes;

namespace ZLDF.MainHost
{
	internal class TestData
	{
		private static int _generatedFightersCounter = 0;
		internal static Fighter GenerateTestFighter()
		{
			Fighter genFighter = new Fighter();

			genFighter.FirstName = _generatedFightersCounter.ToString();
			genFighter.LastName = "Fighter";

			_generatedFightersCounter++;
			return genFighter;
		}
		internal static IEnumerable<Fighter> GenerateTestFighters(int numFighters)
		{
			Fighter[] genFighters = new Fighter[numFighters];

			for (int i = 0; i < numFighters; i++)
			{
				genFighters[i] = GenerateTestFighter();
			}

			return genFighters;
		}

		private static int _generatedTournamentsCounter = 0;
		internal static Tournament GenerateTestTournament(int numFighters, int numNominations)
		{
			Tournament tournament = new Tournament();

			IEnumerable<Fighter> fighters = GenerateTestFighters(numFighters);
			tournament.AddFighters(fighters);

			for (int i = 0; i < numNominations; i++)
			{
				tournament.Nominations.Add(GenerateTestNomination(fighters));
			}
			tournament.Name = $"Tournament {_generatedTournamentsCounter}";

			_generatedTournamentsCounter++;
			return tournament;
		}

		private static int _generatedNominationsCounter = 0;
		internal static Nomination GenerateTestNomination(IEnumerable<Fighter> fighters)
		{
			Nomination nomination = new Nomination();

			nomination.AddFighters(fighters);
			nomination.Name = $"Nomination {_generatedNominationsCounter}";

			_generatedNominationsCounter++;
			return nomination;
		}
	}
}

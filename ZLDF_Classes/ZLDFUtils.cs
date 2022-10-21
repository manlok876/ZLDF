using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZLDF.Classes
{
	public static class ZLDFUtils
	{
		public static IEnumerable<Fighter> GetAllFightersFromDuels(IEnumerable<Duel> duels)
		{
			HashSet<Fighter> fighters = new HashSet<Fighter>();

			foreach (Duel duel in duels)
			{
				fighters.Add(duel.FirstFighter);
				fighters.Add(duel.SecondFighter);
			}

			return fighters;
		}

		public static string GetTSVFromDuels(IEnumerable<Duel> duels)
		{
			string result = "";

			using (StringWriter strWriter = new StringWriter())
			{
				foreach (Duel duel in duels)
				{
					strWriter.WriteLine(GetTSVFromDuel(duel));
				}

				result = strWriter.ToString();
			}

			return result;
		}

		public static string GetTSVFromDuel(Duel duel)
		{
			string result = "";

			using (StringWriter strWriter = new StringWriter())
			{
				strWriter.Write($"{duel.Id}");
				strWriter.Write('\t');
				strWriter.Write($"{duel.TotalTime}");
				strWriter.Write('\t');
				strWriter.Write(GetTSVFromFighter(duel.FirstFighter));
				strWriter.Write('\t');
				strWriter.Write($"{duel.FirstFighterScore}");
				strWriter.WriteLine();

				strWriter.Write($"{duel.Id}");
				strWriter.Write('\t');
				strWriter.Write($"{duel.RemainingTime}");
				strWriter.Write('\t');
				strWriter.Write(GetTSVFromFighter(duel.SecondFighter));
				strWriter.Write('\t');
				strWriter.Write($"{duel.SecondFighterScore}");
				strWriter.WriteLine();

				result = strWriter.ToString();
			}

			return result;
		}

		public static string GetTSVFromFighter(Fighter fighter)
		{
			string result = "";

			using (StringWriter strWriter = new StringWriter())
			{
				strWriter.Write($"{fighter.Id}");
				strWriter.Write('\t');
				strWriter.Write($"{fighter.LastName}\t{fighter.FirstName}");
				strWriter.Write('\t');
				strWriter.Write(fighter.Club?.Name ?? "-");

				result = strWriter.ToString();
			}

			return result;
		}

		public static Fighter? ParseFighterFromTSVString(string fighterString)
		{
			// LastName	FirstName	Club ("-" if no club)

			Regex fighterRegex =
				new Regex(@"(?:(?<fighterId>.{32,38})\s)?(?<lastName>\w+)\s(?<firstName>\w+)\s(?<club>.*)",
				RegexOptions.None, TimeSpan.FromMilliseconds(150));
			Match fighterMatch = fighterRegex.Match(fighterString);

			string fighterLastName = fighterMatch.Result("${lastName}");
			string fighterFirstName = fighterMatch.Result("${firstName}");
			string clubName = fighterMatch.Result("${club}");

			// Create Fighter object with given data and return
			Fighter fighter = new Fighter();
			Guid fighterId;
			bool hasId = Guid.TryParse(fighterMatch.Result("${fighterId}"), out fighterId);
			if (!hasId)
			{
				fighterId = Guid.NewGuid();
			}
			fighter.Id = fighterId;
			fighter.FirstName = fighterFirstName;
			fighter.LastName = fighterLastName;
			fighter.Club = new Club();
			fighter.Club.Name = clubName;

			return fighter;
		}

		public static IEnumerable<Duel> ParseDuelsFromTSVString(string duelsString)
		{
			// duelId	fullTime	fighterId1	LastName1	FirstName1	Club1 ("-" if no club)
			// duelId	remainingTime	fighterId2	LastName2	FirstName2	Club2 ("-" if no club)

			//Regex duelRegex =
			//	new Regex(@"^(?<duelId>\S{32,38})\s(?<fullTime>\S*)\s(?:(?<fighterId1>.{32,38})\s)?(?<lastName1>\w+)\s(?<firstName1>\w+)\s(?<club1>.*)\s(?<score1>\d+)\s*^\k<duelId>\s(?<remainingTime>\S*)\s(?:(?<fighterId2>.{32,38})\s)?(?<lastName2>\w+)\s(?<firstName2>\w+)\s(?<club2>.*)\s(?<score2>\d+)",
			//	RegexOptions.Multiline, TimeSpan.FromMilliseconds(150));

			string duelFormat =
				@"^(?<duelId>.{32,38})\s(?<totalTime>\S*)\s" +
				@"(?:(?<fighterId1>.{32,38})\s)?(?<lastName1>\w+)\s(?<firstName1>\w+)\s" +
				@"(?<club1>.*)\s(?<score1>\d+)\s*" +
				@"^\k<duelId>\s(?<remainingTime>\S*)\s" +
				@"(?:(?<fighterId2>.{32,38})\s)?(?<lastName2>\w+)\s(?<firstName2>\w+)\s" +
				@"(?<club2>.*)\s(?<score2>\d+)\s";

			Regex duelRegex =
				new Regex(duelFormat, RegexOptions.Multiline, TimeSpan.FromMilliseconds(150));

			MatchCollection duelMatches = duelRegex.Matches(duelsString);

			Dictionary<Guid, Fighter> fighters = new Dictionary<Guid, Fighter>();
			Fighter GetOrCreateFighter(Guid id, string lastName, string firstName, Club club)
			{
				if (fighters.ContainsKey(id))
				{
					return fighters[id];
				}

				Fighter fighter = new Fighter();
				fighter.Id = id;
				fighter.LastName = lastName;
				fighter.FirstName = firstName;
				fighter.Club = club;

				fighters.Add(id, fighter);

				return fighter;
			}

			Dictionary<string, Club> clubs = new Dictionary<string, Club>();
			Club GetOrCreateClub(string clubName)
			{
				if (clubs.ContainsKey(clubName))
				{
					return clubs[clubName];
				}

				Club club = new Club();
				club.Name = clubName;

				clubs.Add(clubName, club);

				return club;
			}

			List<Duel> duels = new List<Duel>();
			foreach (Match duelMatch in duelMatches)
			{
				Guid fighterId1;
				Guid.TryParse(duelMatch.Result("${fighterId1}"), out fighterId1);
				string fighterLastName1 = duelMatch.Result("${lastName1}");
				string fighterFirstName1 = duelMatch.Result("${firstName1}");
				string clubName1 = duelMatch.Result("${club1}");
				Club club1 = GetOrCreateClub(clubName1);

				Guid fighterId2;
				Guid.TryParse(duelMatch.Result("${fighterId2}"), out fighterId2);
				string fighterLastName2 = duelMatch.Result("${lastName2}");
				string fighterFirstName2 = duelMatch.Result("${firstName2}");
				string clubName2 = duelMatch.Result("${club2}");
				Club club2 = GetOrCreateClub(clubName2);

				Duel duel = new Duel();

				Guid duelId;
				Guid.TryParse(duelMatch.Result("${duelId}"), out duelId);
				duel.Id = duelId;

				TimeSpan totalTime;
				TimeSpan.TryParse(duelMatch.Result("${totalTime}"), out totalTime);
				duel.TotalTime = totalTime;

				TimeSpan remainingTime;
				TimeSpan.TryParse(duelMatch.Result("${remainingTime}"), out remainingTime);
				duel.RemainingTime = remainingTime;

				duel.FirstFighter =
					GetOrCreateFighter(fighterId1, fighterLastName1, fighterFirstName1, club1);
				duel.FirstFighterScore = int.Parse(duelMatch.Result("${score1}"));

				duel.SecondFighter =
					GetOrCreateFighter(fighterId2, fighterLastName2, fighterFirstName2, club2);
				duel.SecondFighterScore = int.Parse(duelMatch.Result("${score2}"));

				duels.Add(duel);
			}

			return duels;
		}

	}
}

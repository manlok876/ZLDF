using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.Classes;
using ZLDF.Classes.Matchmaking;
using ZLDF.MainHost.Data;
using ZLDF.MainHost.Data.EF;
using ZLDF.MainHost.Views.Tournament;

namespace ZLDF.MainHost.Testing.ViewModels
{
	internal class RatingTestViewModel : BindableBase
	{
		public class FighterStats
		{
			public Fighter Fighter { get; set; }

			public int WinCount { get; set; } = 0;
			public int DrawCount { get; set; } = 0;
			public int LossCount { get; set; } = 0;
			public float TotalScore { get; set; } = 0;
			public float TotalOpponentsScore { get; set; } = 0;


			public FighterStats(Fighter fighter)
			{
				Fighter = fighter;
			}
		}

		private Dictionary<string, Club> _clubMap = new Dictionary<string, Club>();
		private void AddClub(Club club)
		{
			_clubMap.Add(club.Name, club);
		}
		private Club? GetClubByName(string clubName)
		{
			if (_clubMap.ContainsKey(clubName))
			{
				return _clubMap[clubName];
			}
			return null;
		}
		public IEnumerable<Club> Clubs
		{
			get
			{
				return _clubMap.Values;
			}
		}

		private IEnumerable<Fighter>? _fighters;
		private Dictionary<string, Fighter> _fighterMap = new Dictionary<string, Fighter>();

		public string DuelsListString
		{
			get;
			set;
		} = "";

		private IEnumerable<Duel>? _duels;

		private IEnumerable<FighterStats>? _ratings;

		public string RatingsListString
		{
			get
			{
				string result = "";
				if (_ratings == null)
				{
					return result;
				}

				using (StringWriter strWriter = new StringWriter())
				{
					foreach (FighterStats fighterStat in _ratings)
					{
						strWriter.Write($"{fighterStat.Fighter.LastName} {fighterStat.Fighter.FirstName}");
						strWriter.Write('\t');
						strWriter.Write(fighterStat.WinCount);
						strWriter.Write('\t');
						strWriter.Write(fighterStat.LossCount);
						strWriter.Write('\t');
						strWriter.Write(fighterStat.DrawCount);
						strWriter.Write('\t');
						strWriter.Write(fighterStat.TotalScore);
						strWriter.Write('\t');
						strWriter.Write(fighterStat.TotalOpponentsScore);
						strWriter.WriteLine();
					}
					result = strWriter.ToString();
				}

				return result;
			}
			set
			{
			}
		}

		#region Commands
		public ICommand BuildRatingsCommand { get; private set; }
		public void BuildRatings()
		{
			_duels = ParseFightsFromTSV(DuelsListString);
			_ratings = GetFightersRatings(_duels);
			RaisePropertyChanged(nameof(RatingsListString));
		}

		#endregion

		#region Parsing
		public IEnumerable<FighterStats> GetFightersRatings(IEnumerable<Duel> duels)
		{
			Dictionary<Fighter, FighterStats> ratingByFighter =
				new Dictionary<Fighter, FighterStats>();

			foreach (Duel duel in duels)
			{
				if (!ratingByFighter.ContainsKey(duel.FirstFighter))
				{
					ratingByFighter.Add(duel.FirstFighter, new FighterStats(duel.FirstFighter));
				}
				if (!ratingByFighter.ContainsKey(duel.SecondFighter))
				{
					ratingByFighter.Add(duel.SecondFighter, new FighterStats(duel.SecondFighter));
				}

				FighterStats firstFighterRating = ratingByFighter[duel.FirstFighter];
				FighterStats secondFighterRating = ratingByFighter[duel.SecondFighter];

				if (duel.FirstFighterScore > duel.SecondFighterScore)
				{
					firstFighterRating.WinCount++;
					secondFighterRating.LossCount++;
				}
				else if (duel.FirstFighterScore < duel.SecondFighterScore)
				{
					firstFighterRating.LossCount++;
					secondFighterRating.WinCount++;
				}
				else
				{
					firstFighterRating.DrawCount++;
					secondFighterRating.DrawCount++;
				}

				firstFighterRating.TotalScore += duel.FirstFighterScore;
				firstFighterRating.TotalOpponentsScore += duel.SecondFighterScore;

				secondFighterRating.TotalScore += duel.SecondFighterScore;
				secondFighterRating.TotalOpponentsScore += duel.FirstFighterScore;
			}
			List<FighterStats> result = new List<FighterStats>(ratingByFighter.Values);
			return result;
		}

		public IEnumerable<Duel> ParseFightsFromTSV(string duelsListString)
		{
			List<Duel> result = new List<Duel>();

			using (StringReader sr = new StringReader(duelsListString))
			{
				List<string> lines = new List<string>();
				string? line;
				while ((line = sr.ReadLine()) != null)
				{
					lines.Add(line);
				}
				while (lines.Count > 1)
				{
					Duel? duel = ParseDuelFromTSVStrings(lines[0], lines[1]);
					if (duel != null)
					{
						result.Add(duel);
					}
					lines.RemoveAt(0);
					lines.RemoveAt(0);
					if (lines.Count > 0)
					{
						// Empty line
						lines.RemoveAt(0);
					}
				}
			}

			return result;
		}

		public Duel? ParseDuelFromTSVStrings(string firstFighterString, string secondFighterString)
		{
			string[] firstFighterData = firstFighterString.Split('\t');
			string[] secondFighterData = secondFighterString.Split('\t');
			int firstFighterScore, secondFighterScore;
			if (!int.TryParse(firstFighterData.Last(), out firstFighterScore) ||
				!int.TryParse(secondFighterData.Last(), out secondFighterScore))
			{
				return null;
			}

			Fighter? firstFighter = ParseFighterFromTSVString(firstFighterString);
			Fighter? secondFighter = ParseFighterFromTSVString(secondFighterString);
			if (firstFighter is null || secondFighter is null)
			{
				return null;
			}

			Duel result = new Duel();
			result.FirstFighter = firstFighter;
			result.FirstFighterScore = firstFighterScore;
			result.SecondFighter = secondFighter;
			result.SecondFighterScore = secondFighterScore;

			return result;
		}

		public Fighter? ParseFighterFromTSVString(string fighterString)
		{
			// LastName FirstName	Club ("-" if no club)
			string[] fighterData = fighterString.Split('\t');
			if (fighterData.Length < 2)
			{
				return null;
			}
			string fighterLastName = fighterData[0];
			string fighterName = fighterData[1];
			string clubName = fighterData[2];
			string fighterFullName = $"{fighterLastName} {fighterName}";

			if (_fighterMap.ContainsKey(fighterFullName))
			{
				return _fighterMap[fighterFullName];
			}

			// Try finding club in previous
			Club? fighterClub = GetClubByName(clubName);
			if (fighterClub == null)
			{
				fighterClub = new Club();
				fighterClub.Name = clubName;
				AddClub(fighterClub);
			}

			// Create Fighter object with given data and return
			Fighter fighter = new Fighter();
			fighter.FirstName = fighterName;
			fighter.LastName = fighterLastName;
			fighter.Club = fighterClub;

			_fighterMap.Add(fighterFullName, fighter);

			return fighter;
		}
		#endregion

		public RatingTestViewModel()
		{
			BuildRatingsCommand = new DelegateCommand(BuildRatings, () => true);
		}
	}
}

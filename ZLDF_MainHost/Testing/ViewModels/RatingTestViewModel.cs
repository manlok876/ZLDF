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

			public int TotalFights
			{
				get
				{
					return WinCount + DrawCount + LossCount;
				}
			}
			public float TotalScoreDifference
			{
				get
				{
					return TotalScore - TotalOpponentsScore;
				}
			}
			public float WinPercentage
			{
				get
				{
					return (float)WinCount / (float)TotalFights;
				}
			}
			public float DrawPercentage
			{
				get
				{
					return (float)DrawCount / (float)TotalFights;
				}
			}
			public float LossPercentage
			{
				get
				{
					return (float)LossCount / (float)TotalFights;
				}
			}
			public float AverageScore
			{
				get
				{
					return TotalScore / (float)TotalFights;
				}
			}
			public float AverageOpponentScore
			{
				get
				{
					return TotalOpponentsScore / (float)TotalFights;
				}
			}
			public float AverageScoreDifference
			{
				get
				{
					return TotalScoreDifference / (float)TotalFights;
				}
			}


			public FighterStats(Fighter fighter)
			{
				Fighter = fighter;
			}
		}

		private float WinWeight = 3;
		private float DrawWeight = 2;
		private float LossWeight = 1;

		float CalculateRating(FighterStats stats)
		{
			return stats.WinCount * WinWeight + stats.DrawCount * DrawWeight + stats.LossCount * LossWeight;
		}

		float CalculateAverageRating(FighterStats stats)
		{
			return CalculateRating(stats) / stats.TotalFights;
		}

		int CompareRatings(FighterStats stats1, FighterStats stats2)
		{
			if (stats1 == stats2)
			{
				return 0;
			}

			float rating1 = CalculateAverageRating(stats1);
			float rating2 = CalculateAverageRating(stats2);

			if (rating1 == rating2)
			{
				return (stats1.AverageScoreDifference > stats2.AverageScoreDifference ? -1 : 1);
			}

			return (rating1 > rating2 ? -1 : 1);
		}

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
						strWriter.Write(ZLDFUtils.GetTSVFromFighter(fighterStat.Fighter));
						strWriter.Write('\t');
						strWriter.Write(CalculateRating(fighterStat));
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

			result.Sort(CompareRatings);

			return result;
		}

		public IEnumerable<Duel> ParseFightsFromTSV(string duelsListString)
		{
			List<Duel> result = new List<Duel>(ZLDFUtils.ParseDuelsFromTSVString(duelsListString));
			return result;
		}

		#endregion

		public RatingTestViewModel()
		{
			BuildRatingsCommand = new DelegateCommand(BuildRatings, () => true);
		}
	}
}

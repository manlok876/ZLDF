using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
	internal class RoundRobinMMTestViewModel : BindableBase
	{
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

		public string FightersListString
		{
			get;
			set;
		} = "";

		private IEnumerable<Fighter>? _fighters;
		public IEnumerable<Fighter> Fighters
		{
			get
			{
				if (_fighters == null)
				{
					ParseFightersList();
					if (_fighters == null)
					{
						return new List<Fighter>();
					}
				}
				return _fighters!;
			}
			private set
			{
				SetProperty(ref _fighters, value);
			}
		}


		private IEnumerable<Fight>? _fights;
		public string FightsListString
		{
			get
			{
				string result = "";
				if (_fights == null)
				{
					return result;
				}

				using (StringWriter strWriter = new StringWriter())
				{
					foreach (Fight fight in _fights)
					{
						Duel? duel = fight as Duel;
						if (duel is null)
						{
							continue;
						}

						string duelTSV = ZLDFUtils.GetTSVFromDuel(duel);
						strWriter.Write(duelTSV);

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
		public ICommand ParseFightersListCommand { get; private set; }
		public void ParseFightersList()
		{
			_fighters = new List<Fighter>(ParseFightersFromTSV(FightersListString));
		}

		public ICommand RunMatchmakingCommand { get; private set; }
		public void RunMatchmaking()
		{
			ParseFightersList();
			if (_fighters is null)
			{
				return;
			}
			_fights = MatchmakingRoundRobin.GetFightsFor(_fighters);
			RaisePropertyChanged(nameof(FightsListString));
		}
		#endregion

		#region Parsing

		public IEnumerable<Fighter> ParseFightersFromTSV(string fightersListString)
		{
			List<Fighter> result = new List<Fighter>();

			using (StringReader sr = new StringReader(fightersListString))
			{
				string? fighterData;
				while ((fighterData = sr.ReadLine()) != null)
				{
					Fighter? fighter = ZLDFUtils.ParseFighterFromTSVString(fighterData);
					if (fighter != null)
					{
						result.Add(fighter);
					}
				}
			}

			return result;
		}

		#endregion

		public RoundRobinMMTestViewModel()
		{
			ParseFightersListCommand = new DelegateCommand(ParseFightersList, () => true);
			RunMatchmakingCommand = new DelegateCommand(RunMatchmaking, () => true);
		}
	}
}

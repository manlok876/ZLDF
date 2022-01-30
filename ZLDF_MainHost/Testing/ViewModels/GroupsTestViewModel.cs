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
	internal class GroupsTestViewModel : BindableBase
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

		private IEnumerable<Group>? _groups;
		public int GroupCount { get; set; } = 1;
		public string GroupsListString
		{
			get
			{
				string result = "";
				if (_groups == null)
				{
					return result;
				}

				using (StringWriter strWriter = new StringWriter())
				{
					int groupIdx = 0;
					foreach (Group group in _groups)
					{
						groupIdx++;
						strWriter.WriteLine("Group " + groupIdx.ToString());
						foreach (Fighter fighter in group.Fighters)
						{
							strWriter.Write($"{fighter.LastName} {fighter.FirstName}");
							strWriter.Write('\t');
							strWriter.Write(fighter.Club?.Name ?? "");
							strWriter.WriteLine();
						}
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

		public ICommand MakeGroupsCommand { get; private set; }
		public void MakeGroups()
		{
			ParseFightersList();
			_groups = MatchmakingGroups.GenerateGroups(Fighters, GroupCount);
			RaisePropertyChanged(nameof(GroupsListString));
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
					Fighter? fighter = ParseFighterFromTSVString(fighterData);
					if (fighter != null)
					{
						result.Add(fighter);
					}
				}
			}

			return result;
		}

		public Fighter? ParseFighterFromTSVString(string fighterString)
		{
			// LastName 	FirstName	Club ("-" if no club)
			string[] fighterData = fighterString.Split('\t');
			if (fighterData.Length != 3)
			{
				return null;
			}
			string fighterName = fighterData[0];
			string fighterLastName = fighterData[1];
			string clubName = fighterData[2];

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

			return fighter;
		}
		#endregion

		internal GroupsTestViewModel()
		{
			ParseFightersListCommand = new DelegateCommand(ParseFightersList, () => true);
			MakeGroupsCommand = new DelegateCommand(MakeGroups, () => true);
		}
	}
}

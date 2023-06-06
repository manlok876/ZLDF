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
						strWriter.WriteLine($"\tГруппа {groupIdx}");
						foreach (Fighter fighter in group.Fighters)
						{
							strWriter.Write(ZLDFUtils.GetTSVFromFighter(fighter));
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
					Fighter? fighter = ZLDFUtils.ParseFighterFromTSVString(fighterData);
					if (fighter != null)
					{
						if (fighter.Club != null)
						{
							// Try finding club in previous
							Club? fighterClub = GetClubByName(fighter.Club.Name);
							if (fighterClub != null)
							{
								fighter.Club = fighterClub;
							}
							else
							{
								AddClub(fighter.Club);
							}
						}
						result.Add(fighter);
					}
				}
			}

			return result;
		}

		#endregion

		internal GroupsTestViewModel()
		{
			ParseFightersListCommand = new DelegateCommand(ParseFightersList, () => true);
			MakeGroupsCommand = new DelegateCommand(MakeGroups, () => true);
		}
	}
}

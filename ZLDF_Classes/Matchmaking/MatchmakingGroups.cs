using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes.Matchmaking
{
	public class MatchmakingGroups : MatchmakingBase
	{
		// isOver => initialized && toursgenerated && all tours ended
		// while !isover should generate new tours

		private List<Tour> _tours;
		private List<Fighter> _fighters;
		private List<Group> _groups;
		private int _groupCount;

		public int GroupCount
		{
			get
			{
				return _groupCount;
			}
			set
			{
				SetProperty(ref _groupCount, value);
			}
		}

		public IEnumerable<Tour> PendingTours
		{
			get { return _tours.Where(t => !t.IsOver && (t.State != EventState.Unknown)); }
		}

		public IEnumerable<Tour> OngoingTours
		{
			get { return _tours.Where(t => t.HasStarted && !t.IsOver); }
		}

		public List<Group> Groups
		{
			get
			{
				return _groups;
			}
			private set
			{
				SetProperty(ref _groups, value);
			}
		}

		public void Init(IEnumerable<Fighter> fighters, int numGroups)
		{
			_fighters = new List<Fighter>(fighters);
			GroupCount = numGroups;
		}

		public static bool AreGroupsValid(IEnumerable<Group> groups)
		{
			foreach (Group group in groups)
			{
				if (!IsGroupValid(group))
				{
					return false;
				}
			}
			return true;
		}

		private static bool IsGroupValid(Group group)
		{
			if (!group.Fighters.Any())
			{
				return false;
			}
			return true;
		}

		public void StartMatchmaking()
		{
			if (!_fighters.Any())
			{
				return;
			}

			if (!AreGroupsValid(Groups))
			{
				Groups = new List<Group>(GenerateGroups(_fighters, 5));
			}

			if (!AreGroupsValid(Groups))
			{
				return;
			}

			_tours = new List<Tour>();
			foreach (Group group in Groups)
			{
				Tour tour = new Tour();
				tour.AddFights(MatchmakingRoundRobin.GetFightsFor(group.Fighters));
				tour.State = EventState.NotStarted;
				_tours.Add(tour);
			}
		}

		public static List<Group> GenerateGroups(IEnumerable<Fighter> allFighters, int numGroups)
		{
			if (numGroups < 1)
			{
				return new List<Group>();
			}

			List<Group> groups = new List<Group>(numGroups);
			for (int groupIdx = 0; groupIdx < numGroups; groupIdx++)
			{
				groups.Add(new Group());
			}

			// Sort by clubs
			Dictionary<Club, List<Fighter>> fightersByClub = new Dictionary<Club,List<Fighter>>();
			List<Fighter> fightersWithoutClub = new List<Fighter>();
			foreach (Fighter clubMember in allFighters)
			{
				if (clubMember.Club == null)
				{
					fightersWithoutClub.Add(clubMember);
					continue;
				}
				if (!fightersByClub.ContainsKey(clubMember.Club))
				{
					fightersByClub[clubMember.Club] = new List<Fighter>();
				}
				fightersByClub[clubMember.Club].Add(clubMember);
			}

			// For each club put 1 fighter in each group
			// (if there is already a fighter from the club in each -
			// choose smallest group)
			foreach (Club club in fightersByClub.Keys)
			{
				foreach (Fighter clubMember in fightersByClub[club])
				{
					bool GroupHasNoClubMates(Group targetGroup)
					{
						foreach (Fighter groupFighter in targetGroup.Fighters)
						{
							if (clubMember.Club == groupFighter.Club)
							{
								return false;
							}
						}
						return true;
					}

					IEnumerable<Group> freeGroups = groups.Where(GroupHasNoClubMates);
					if (!freeGroups.Any())
					{
						freeGroups = groups;
					}
					Group? bestGroup = freeGroups.MinBy((group) => group.Fighters.Count);
					Trace.Assert(bestGroup != null);
					bestGroup?.AddFighter(clubMember);
				}
			}
			foreach (Fighter clublessFighter in fightersWithoutClub)
			{
				Group? bestGroup = groups.MinBy((group) => group.Fighters.Count);
				Trace.Assert(bestGroup != null);
				bestGroup?.AddFighter(clublessFighter);
			}

			return groups;
		}

		public MatchmakingGroups()
		{
			_tours = new List<Tour>();
			_fighters = new List<Fighter>();
			_groups = new List<Group>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes.Matchmaking
{
	public enum BracketMode
	{
		InOrder,
		FirstWithLast
	}

	public class MatchmakingBracket : MatchmakingBase
	{
		private List<Fighter> _fighters;
		private List<Tour> _tours;

		public BracketMode BracketMode { get; private set; }
		public List<Fighter> Fighters
		{
			get { return _fighters; }
			private set
			{
				SetProperty(ref _fighters, value);
			}
		}
		public IEnumerable<Fight> GetFightsFor()
		{
			List<Fight> result = new List<Fight>();

			return result;
		}

		public IEnumerable<Tour> PendingTours
		{
			get { return _tours.Where(t => !t.IsOver && (t.State != EventState.Unknown)); }
		}

		public IEnumerable<Tour> OngoingTours
		{
			get { return _tours.Where(t => t.HasStarted && !t.IsOver); }
		}

		public void Init(IEnumerable<Fighter> fighters)
		{
			_fighters = new List<Fighter>(fighters);
		}

		public bool IsFinished
		{
			get
			{
				return false;
			}
		}

		public MatchmakingBracket()
		{
			BracketMode = BracketMode.InOrder;
			_fighters = new List<Fighter>();
			_tours = new List<Tour>();
		}
	}
}

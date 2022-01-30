using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;
using ZLDF.Classes.Matchmaking;

namespace ZLDF.Classes
{
	public class Nomination : Event
	{
		private EventState _nominationState = EventState.Unknown;
		private string _name;

		public override EventState State
		{
			get { return _nominationState; }
			set
			{
				SetProperty(ref _nominationState, value);
			}
		}
		public string Name
		{
			get { return _name; }
			set
			{
				SetProperty(ref _name, value);
			}
		}

		// Participants
		private List<Fighter> _fighters = new List<Fighter>();

		public List<Fighter> Fighters
		{
			get { return _fighters; }
			set
			{
				SetProperty(ref _fighters, value);
			}
		}
		// TODO: check uniqueness
		public void AddFighter(Fighter newFighter)
		{
			_fighters.Add(newFighter);
			RaisePropertyChanged(nameof(Fighters));
		}
		public void AddFighters(IEnumerable<Fighter> newFighters)
		{
			foreach (var fighter in newFighters)
			{
				_fighters.Add(fighter);
			}
			RaisePropertyChanged(nameof(Fighters));
		}

		public void RemoveFighter(Fighter fighterToRemove)
		{
			bool bSuccess = _fighters.Remove(fighterToRemove);
			if (bSuccess)
			{
				RaisePropertyChanged(nameof(Fighters));
			}
		}
		public void RemoveFighters(IEnumerable<Fighter> fightersToRemove)
		{
			bool bSuccess = false;
			foreach (var fighter in fightersToRemove)
			{
				bSuccess = bSuccess || _fighters.Remove(fighter);
			}
			if (bSuccess)
			{
				RaisePropertyChanged(nameof(Fighters));
			}
		}

		public void ClearFighters()
		{
			if (_fighters.Count < 1)
			{
				return;
			}
			_fighters.Clear();
			RaisePropertyChanged(nameof(Fighters));
		}

		public MatchmakingBase Matchmaking
		{
			get;
			private set;
		}

		public void SetMatchmaking(MatchmakingBase matchmaker)
		{
			if (Matchmaking != null)
			{
				return;
			}
			Matchmaking = matchmaker;
			RaisePropertyChanged(nameof(Matchmaking));
		}

		// Fight rules

		public Nomination()
		{
			_name = "Some Nomination";
		}
	}
}

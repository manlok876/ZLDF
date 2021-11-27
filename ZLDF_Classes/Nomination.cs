using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public class Nomination : Event
	{
		private string _name;
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

		public ImmutableArray<Fighter> Fighters
		{
			get { return _fighters.ToImmutableArray(); }
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

		// Fight rules

		// Matchmaking

		public ImmutableArray<Fight> Fights { get; }


		// Judges?
		public Nomination()
		{
			_name = "Some Nomination";
		}

	}
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ZLDF.Classes
{
	public class Tournament : Event
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

		private List<Fighter> _fighters = new List<Fighter>();

		public ImmutableArray<Fighter> Fighters
		{
			get { return _fighters.ToImmutableArray(); }
		}
		// TODO: check uniqueness
		public void AddFighter(Fighter newFighter)
		{
			_fighters.Add(newFighter);
			RaisePropertyChanged("Fighters");
		}
		public void AddFighters(IEnumerable<Fighter> newFighters)
		{
			foreach (var fighter in newFighters)
			{
				_fighters.Add(fighter);
			}
			RaisePropertyChanged("Fighters");
		}
		
		public void RemoveFighter(Fighter fighterToRemove)
		{
			bool bSuccess = _fighters.Remove(fighterToRemove);
			if (bSuccess)
			{
				RaisePropertyChanged("Fighters");
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
				RaisePropertyChanged("Fighters");
			}
		}

		public void ClearFighters()
		{
			if (_fighters.Count < 1)
			{
				return;
			}
			_fighters.Clear();
			RaisePropertyChanged("Fighters");
		}

		public Tournament()
		{
			_name = "Some Tournament";
		}
	}
}

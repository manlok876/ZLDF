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

		private List<Nomination> _nominations = new List<Nomination>();
		public List<Nomination> Nominations
		{
			get { return _nominations; }
			set
			{
				SetProperty(ref _nominations, value);
			}
		}

		private List<Fighter> _fighters = new List<Fighter>();

		public List<Fighter> Fighters
		{
			get { return _fighters; }
			private set
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

		public Tournament()
		{
			_name = "Some Tournament";
		}
	}
}

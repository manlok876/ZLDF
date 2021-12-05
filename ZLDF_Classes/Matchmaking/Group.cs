using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ZLDF.Classes.Matchmaking
{
	public class Group : BindableBase
	{
		public Guid Id { get; private set; }

		private List<Fighter> _fighters;

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

		public Group()
		{
			_fighters = new List<Fighter>();
		}

		public Group(IEnumerable<Fighter> groupFighters)
		{
			_fighters = new List<Fighter>(groupFighters);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ZLDF.Classes
{
	public class Tournament : Event
	{
		private List<Fighter> _fighters = new List<Fighter>();

		public ImmutableArray<Fighter> Fighters
		{
			get { return _fighters.ToImmutableArray<Fighter>(); }
		}
		// TODO: check uniqueness
		public void AddFighter(Fighter NewFighter)
		{
			_fighters.Add(NewFighter);
			RaisePropertyChanged("Fighters");
		}
		public void AddFighters(IEnumerable<Fighter> NewFighters)
		{
			foreach (var Fighter in NewFighters)
			{
				_fighters.Add(Fighter);
			}
			RaisePropertyChanged("Fighters");
		}
		
		public void RemoveFighter(Fighter FighterToRemove)
		{
			bool bSuccess = _fighters.Remove(FighterToRemove);
			if (bSuccess)
			{
				RaisePropertyChanged("Fighters");
			}
		}
		public void RemoveFighters(IEnumerable<Fighter> FightersToRemove)
		{
			bool bSuccess = false;
			foreach (var Fighter in FightersToRemove)
			{
				bSuccess = bSuccess || _fighters.Remove(Fighter);
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
		}
	}
}

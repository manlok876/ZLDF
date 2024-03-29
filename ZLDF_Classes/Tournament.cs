﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Immutable;
using System.Text;

namespace ZLDF.Classes
{
	public class Tournament : Event
	{
		private EventState _tournamentState = EventState.Unknown;
		public override EventState State
		{
			get { return _tournamentState; }
			set
			{
				SetProperty(ref _tournamentState, value);
			}
		}

		private string _name = "Some Tournament";
		public string Name
		{
			get { return _name; }
			set
			{
				SetProperty(ref _name, value);
			}
		}

		private ObservableCollection<Nomination> _nominations = new ObservableCollection<Nomination>();
		public ObservableCollection<Nomination> Nominations
		{
			get { return _nominations; }
			set
			{
				SetProperty(ref _nominations, value);
			}
		}

		private ObservableCollection<Fighter> _fighters = new ObservableCollection<Fighter>();
		public ObservableCollection<Fighter> Fighters
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
		}
	}
}

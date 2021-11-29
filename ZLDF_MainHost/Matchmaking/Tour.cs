using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Classes;

namespace ZLDF.MainHost.Matchmaking
{
	internal class Tour : Event
	{
		private EventState _tourState = EventState.Unknown;
		private List<Fight> _fights = new List<Fight>();

		public override EventState State
		{
			get
			{
				return _tourState;
			}
			set
			{
				SetProperty(ref _tourState, value);
			}
		}
		public List<Fight> Fights
		{
			get { return _fights; }
			private set
			{
				SetProperty(ref _fights, value);
			}
		}

		// TODO: check uniqueness
		public void AddFight(Fight newFight)
		{
			_fights.Add(newFight);
			RaisePropertyChanged(nameof(Fights));
		}
		public void AddFights(IEnumerable<Fight> newFights)
		{
			foreach (var fight in newFights)
			{
				_fights.Add(fight);
			}
			RaisePropertyChanged(nameof(Fights));
		}

		public void RemoveFight(Fight fightToRemove)
		{
			bool bSuccess = _fights.Remove(fightToRemove);
			if (bSuccess)
			{
				RaisePropertyChanged(nameof(Fights));
			}
		}
		public void RemoveFights(IEnumerable<Fight> fightsToRemove)
		{
			bool bSuccess = false;
			foreach (var fight in fightsToRemove)
			{
				bSuccess = bSuccess || _fights.Remove(fight);
			}
			if (bSuccess)
			{
				RaisePropertyChanged(nameof(Fights));
			}
		}

		public void ClearFights()
		{
			if (_fights.Count < 1)
			{
				return;
			}
			_fights.Clear();
			RaisePropertyChanged(nameof(Fights));
		}

		public Tour()
		{
		}
	}
}

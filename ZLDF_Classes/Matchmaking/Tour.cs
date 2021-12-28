using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes.Matchmaking
{
	public class Tour : Event
	{
		private EventState _tourState = EventState.Unknown;
		private ObservableCollection<Fight> _fights = new ObservableCollection<Fight>();

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
		public ObservableCollection<Fight> Fights
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
		}
		public void AddFights(IEnumerable<Fight> newFights)
		{
			foreach (var fight in newFights)
			{
				_fights.Add(fight);
			}
		}

		public void RemoveFight(Fight fightToRemove)
		{
			_fights.Remove(fightToRemove);
		}
		public void RemoveFights(IEnumerable<Fight> fightsToRemove)
		{
			foreach (var fight in fightsToRemove)
			{
				_fights.Remove(fight);
			}
		}

		public void ClearFights()
		{
			_fights.Clear();
		}

		public Tour()
		{
		}
	}
}

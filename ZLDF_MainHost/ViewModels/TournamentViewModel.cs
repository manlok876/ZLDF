using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.Classes;
using ZLDF.MainHost.Matchmaking;

namespace ZLDF.MainHost.ViewModels
{
	internal class TournamentViewModel : BindableBase
	{
		private Tournament _model;
		public Tournament Model {
			get
			{
				return _model;
			}
			private set
			{
				SetProperty(ref _model, value);
			}
		}
		public ObservableCollection<Fight> Fights { get; set; }

		public int NumFighters
		{
			get { return Model.Fighters.Count; }
			set
			{
				List<Fighter> GenFighters = new List<Fighter>(value);
				for (int i = 0; i < value; i++)
				{
					Fighter NewFighter = new Fighter { FirstName = i.ToString() };
					GenFighters.Add(NewFighter);
				}
				Model.Fighters = GenFighters;
			}
		}

		public ICommand GenFightsCommand { get; private set; }

		public TournamentViewModel()
		{
			_model = new Tournament();
			NumFighters = 5;
			Fights = new ObservableCollection<Fight>();
			GenerateFights();
			GenFightsCommand = new DelegateCommand(GenerateFights, () => true);
		}

		public void GenerateFights()
		{
			Fights.Clear();
			List<Fight> NewFights = MatchmakingRoundRobin.GetFightsFor(Model.Fighters.ToArray());
			Fights.AddRange(NewFights);
		}
	}
}

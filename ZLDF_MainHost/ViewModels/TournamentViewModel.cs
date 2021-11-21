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

		private ObservableCollection<Fight> _fights = new ObservableCollection<Fight>();
		public ObservableCollection<Fight> Fights
		{
			get { return _fights; }
		}

		public int NumFighters
		{
			get { return Model.Fighters.Length; }
			set
			{
				List<Fighter> genFighters = new List<Fighter>(value);
				for (int fighterIdx = 0; fighterIdx < value; fighterIdx++)
				{
					Fighter newFighter = new Fighter { FirstName = fighterIdx.ToString() };
					genFighters.Add(newFighter);
				}
				Model.ClearFighters();
				Model.AddFighters(genFighters);
			}
		}

		public ICommand GenFightsCommand { get; private set; }

		public TournamentViewModel(Tournament tournamentModel)
		{
			_model = tournamentModel;
			NumFighters = 5;
			GenerateFights();
			GenFightsCommand = new DelegateCommand(GenerateFights, () => true);
		}

		public void GenerateFights()
		{
			Fights.Clear();
			List<Fight> newFights = MatchmakingRoundRobin.GetFightsFor(Model.Fighters.ToArray());
			Fights.AddRange(newFights);
		}
	}
}

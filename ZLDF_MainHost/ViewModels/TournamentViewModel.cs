using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

		public ImmutableArray<NominationViewModel> Nominations
		{
			get;
			private set;
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
				foreach (NominationViewModel nomination in Nominations)
				{
					nomination.Model.ClearFighters();
					nomination.Model.AddFighters(genFighters);
				}
			}
		}

		public ICommand GenFightsCommand { get; private set; }
		public void GenerateFights()
		{
			foreach (NominationViewModel nomination in Nominations)
			{
				nomination.Model.Fights.Clear();
				List<Fight> newFights = MatchmakingRoundRobin.GetFightsFor(Model.Fighters.ToArray());
				nomination.Fights.AddRange(newFights);
			}
		}

		// Our matchmaking
		// Break into groups -> round-robin in each group -> get X best -> bracket between them

		public TournamentViewModel(Tournament tournamentModel)
		{
			_model = tournamentModel;
			List<NominationViewModel> nominationViewModels = new List<NominationViewModel>();
			foreach (Nomination nominationModel in Model.Nominations)
			{
				nominationViewModels.Add(new NominationViewModel(nominationModel));
			}
			Nominations = nominationViewModels.ToImmutableArray();
			NumFighters = 5;
			GenerateFights();
			GenFightsCommand = new DelegateCommand(GenerateFights, () => true);
		}
	}
}

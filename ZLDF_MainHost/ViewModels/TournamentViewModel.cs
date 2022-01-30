using System;
using System.Diagnostics;
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
using ZLDF.Classes.Matchmaking;
using ZLDF.MainHost.Data;
using ZLDF.MainHost.Data.EF;
using ZLDF.MainHost.Views.Tournament;
using Microsoft.EntityFrameworkCore;

namespace ZLDF.MainHost.ViewModels
{
	internal class TournamentViewModel : BindableBase
	{
		private TournamentDbContext _dbContext;

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

		public ObservableCollection<NominationViewModel> Nominations
		{
			get;
			private set;
		}

		public int NumFighters
		{
			get { return Model.Fighters.Count; }
			set
			{
				List<Fighter> genFighters = new List<Fighter>(TestData.GenerateTestFighters(value));
				Model.ClearFighters();
				Model.AddFighters(genFighters);
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
				nomination.Fights.Clear();
				List<Fight> newFights = new List<Fight>(MatchmakingRoundRobin.GetFightsFor(Model.Fighters.ToArray()));
				nomination.Fights.AddRange(newFights);
			}
		}

		public ICommand CreateNominationCommand { get; private set; }
		public void CreateNomination()
		{
			Nomination newNomination = TestData.GenerateTestNomination(_model.Fighters);
			_model.Nominations.Add(newNomination);
			_dbContext.SaveChanges();
			Nominations.Add(new NominationViewModel(newNomination));
		}

		#region FightersList
		private Fighter? _selectedFighter;
		public Fighter? SelectedFighter
		{
			get { return _selectedFighter; }
			set
			{
				if (_selectedFighter != null && _selectedFighter != value)
				{
					_dbContext.SaveChanges();
				}
				SetProperty(ref _selectedFighter, value);
			}
		}

		public ICommand CreateFighterCommand { get; private set; }
		public void CreateFighter()
		{
			Fighter fighter = new Fighter();
			Model.AddFighter(fighter);
			SelectedFighter = fighter;
			// Other related logic, e.g. adding to nominations
		}

		public ICommand DeleteFighterCommand { get; private set; }
		public void DeleteSelectedFighter()
		{
			if (SelectedFighter == null)
			{
				return;
			}
			Model.RemoveFighter(SelectedFighter);
			SelectedFighter = null;
			// Other related logic, e.g. removing from nominations
		}
		#endregion

		// Our matchmaking
		// Break into groups -> round-robin in each group -> get X best -> bracket between them

		public TournamentViewModel(TournamentConnection tournamentConnection)
		{
			_dbContext = new TournamentDbContext(tournamentConnection);
			// Eagerly load everything
			// Yes, give it to me, Secretary-sama~ <3
			Tournament? loadedTournament = _dbContext.Tournaments.
				Include(t => t.Fighters).
				Include(t => t.Nominations).ThenInclude(n => n.Fighters).
				FirstOrDefault();
			if (loadedTournament != null)
			{
				_model = loadedTournament;
			}
			else
			{
				Trace.WriteLine("Loading tournament failed");
				_model = new Tournament();
				_dbContext.Add(_model);
				_dbContext.SaveChanges();
			}

			Nominations = new ObservableCollection<NominationViewModel>();
			foreach (Nomination nominationModel in Model.Nominations)
			{
				Nominations.Add(new NominationViewModel(nominationModel));
			}
			
			// NumFighters = 5;
			// GenerateFights();
			GenFightsCommand = new DelegateCommand(GenerateFights, () => true);
			CreateNominationCommand = new DelegateCommand(CreateNomination, () => true);
			CreateFighterCommand = new DelegateCommand(CreateFighter, () => true);
			DeleteFighterCommand = new DelegateCommand(DeleteSelectedFighter, () => true);
		} 
	}
}

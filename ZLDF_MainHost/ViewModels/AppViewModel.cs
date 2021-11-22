using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.Classes;
using ZLDF.MainHost.Views;

namespace ZLDF.MainHost.ViewModels
{
	internal class AppViewModel : BindableBase
	{
		private List<Tournament> _tournaments;
		private TournamentViewModel? _selectedTournament;

		public ImmutableArray<Tournament> Tournaments
		{
			get { return _tournaments.ToImmutableArray(); }
		}
		public TournamentViewModel? SelectedTournament
		{
			get { return _selectedTournament; }
			private set
			{
				SetProperty(ref _selectedTournament, value);
			}
		}

		public ICommand CreateTournamentCommand { get; private set; }
		private void CreateTournament(string name)
		{
			Tournament newTournament = TestData.GenerateTestTournament(7, 3);
			_tournaments.Add(newTournament);
			RaisePropertyChanged("Tournaments");
		}

		public ICommand SelectTournamentCommand { get; private set; }
		private void SelectTournament(Tournament tournament)
		{
			if (tournament == null)
			{
				return;
			}
			SelectedTournament = new TournamentViewModel(tournament);
		}

		public ICommand OpenSelectedTournamentCommand { get; private set; }
		private bool CanOpenSelectedTournament() => _selectedTournament != null;
		private void OpenSelectedTournament()
		{
			TournamentView tournamentView = new TournamentView();
			tournamentView.DataContext = SelectedTournament;
			tournamentView.Show();
		}

		public ICommand OpenTournamentCommand { get; private set; }
		private void OpenTournament(Tournament tournament)
		{
			SelectTournament(tournament);
			if (CanOpenSelectedTournament())
			{
				OpenSelectedTournament();
			}
		}

		public AppViewModel()
		{
			_tournaments = new List<Tournament>();
			_selectedTournament = null;

			CreateTournamentCommand = new DelegateCommand<string>(CreateTournament, (name) => true);
			SelectTournamentCommand = new DelegateCommand<Tournament>(SelectTournament, (tournament) => true);
			OpenSelectedTournamentCommand = new DelegateCommand(OpenSelectedTournament, CanOpenSelectedTournament);
			OpenTournamentCommand = new DelegateCommand<Tournament>(OpenTournament, (tournament) => true);
		}

	}
}

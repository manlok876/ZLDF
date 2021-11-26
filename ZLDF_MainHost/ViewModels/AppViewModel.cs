using System;
using System.IO;
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
using ZLDF.MainHost.Data;
using ZLDF.MainHost.Data.EF;

namespace ZLDF.MainHost.ViewModels
{
	internal class AppViewModel : BindableBase
	{
		private List<TournamentConnection> _tournaments;
		private TournamentConnection? _selectedTournament;

		public ImmutableArray<TournamentConnection> Tournaments
		{
			get { return _tournaments.ToImmutableArray(); }
		}
		public TournamentConnection? SelectedTournament
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
			TournamentConnection newTournament = TestData.GenerateTestConnection();
			using (TournamentDbContext tournamentDbContext = new TournamentDbContext(newTournament))
			{
				tournamentDbContext.Database.EnsureCreated();
			}
			_tournaments.Add(newTournament);
			RaisePropertyChanged("Tournaments");
		}

		public ICommand SelectTournamentCommand { get; private set; }
		private void SelectTournament(TournamentConnection tournament)
		{
			SelectedTournament = tournament;
		}

		public ICommand OpenSelectedTournamentCommand { get; private set; }
		private bool CanOpenSelectedTournament() => _selectedTournament != null;
		private void OpenSelectedTournament()
		{
			if (SelectedTournament == null)
			{
				return;
			}
			TournamentView tournamentView = new TournamentView();
			tournamentView.DataContext = new TournamentViewModel(SelectedTournament);
			tournamentView.Show();
		}

		public ICommand OpenTournamentCommand { get; private set; }
		private void OpenTournament(TournamentConnection tournament)
		{
			SelectTournament(tournament);
			if (CanOpenSelectedTournament())
			{
				OpenSelectedTournament();
			}
		}

		public AppViewModel()
		{
			_tournaments = new List<TournamentConnection>();

			// All SQLite tournaments
			Directory.CreateDirectory("Tournaments");
			IEnumerable<string> localTournamentFiles = Directory.EnumerateFiles("Tournaments", "*.db");
			foreach (string file in localTournamentFiles)
			{
				Console.WriteLine(file);
				_tournaments.Add(new TournamentConnection(file));
			}

			_selectedTournament = null;

			// TODO: change to create new db file
			CreateTournamentCommand = new DelegateCommand<string>(CreateTournament, (name) => true);
			
			SelectTournamentCommand = new DelegateCommand<TournamentConnection>(SelectTournament, (tournament) => true);
			OpenSelectedTournamentCommand = new DelegateCommand(OpenSelectedTournament, CanOpenSelectedTournament);

			// TODO: change to add ability to load from different file
			// OpenTournamentFileCommand
			OpenTournamentCommand = new DelegateCommand<TournamentConnection>(OpenTournament, (tournament) => true);
		}

	}
}

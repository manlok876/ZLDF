using System;
using System.Collections.Generic;
using System.Linq;

using Prism.Mvvm;
using Prism.Commands;
using Prism.Ioc;

using ZLDF.Core;
using ZLDF.DataAccess;
using ZLDF.Temp.EF;
using ZLDF.Temp.Services;
using Prism.Regions;
using ZLDF.WPF;

namespace ZLDF.MainHost.ViewModels
{
	public class HostStartMenuViewModel : BindableBase
	{
		private readonly ITournamentDatabase _tournamentDatabase;
		private readonly ITournamentService _tournamentService;
		private readonly IRegionManager _regionManager;

		public HostStartMenuViewModel(
			ITournamentDatabase tournamentDatabase,
			ITournamentService tournamentService,
			IRegionManager regionManager)
		{
			_tournamentDatabase = tournamentDatabase;
			_tournamentService = tournamentService;
			_regionManager = regionManager;
		}

		public string NewTournamentTitle { get; set; } = "New Tournament";

		private DelegateCommand<string>? _createTournamentCommand;
		public DelegateCommand<string> CreateTournamentCommand =>
			_createTournamentCommand ??= new DelegateCommand<string>(CreateTournament);

		public void CreateTournament(string filePath)
		{
			DatabaseReference dbReference = new DatabaseReference(filePath);
			_tournamentDatabase.ConnectToDatabase(dbReference);

			Tournament createdTournament = _tournamentService.CreateNewTournament();
			_tournamentService.SetTournamentTitle(NewTournamentTitle);

			_regionManager.RequestNavigate(RegionNames.MainHostRegion, "TournamentView");
		}

		private DelegateCommand<string>? _loadTournamentCommand;
		public DelegateCommand<string> LoadTournamentCommand =>
			_loadTournamentCommand ??= new DelegateCommand<string>(LoadTournament);

		public void LoadTournament(string filePath)
		{
			DatabaseReference dbReference = new DatabaseReference(filePath);
			_tournamentDatabase.ConnectToDatabase(dbReference);

			Tournament loadedTournament = _tournamentService.LoadTournament();

			if (loadedTournament == null)
			{
				return;
			}

			_regionManager.RequestNavigate(RegionNames.MainHostRegion, "TournamentView");
		}
	}
}

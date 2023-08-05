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

namespace ZLDF.MainHost.ViewModels
{
	public class HostStartMenuViewModel : BindableBase
	{
		private readonly ITournamentDatabase _tournamentDatabase;

		public HostStartMenuViewModel(ITournamentDatabase tournamentDatabase)
		{
			_tournamentDatabase = tournamentDatabase;
		}

		public string NewTournamentTitle { get; set; } = "New Tournament";

		//private DelegateCommand<string>? _createTournamenCommand;
		//public DelegateCommand<string> CreateTournamentCommand =>
		//	_createTournamenCommand ??= new DelegateCommand<string>(CreateTournament);

		public void CreateTournament(string filePath)
		{
			Tournament createdTournament = new Tournament();
			createdTournament.Title = NewTournamentTitle;

			using (TournamentDbContext tournamentDbContext = new TournamentDbContext(filePath))
			{
				// Wipe, if needed we should ask for overwrite in dialog
				tournamentDbContext.Database.EnsureDeleted();
				tournamentDbContext.Database.EnsureCreated();
				tournamentDbContext.Add(createdTournament);
				tournamentDbContext.SaveChanges();
			}

			_tournamentDatabase.Init(createdTournament);

		}

		//private DelegateCommand<string>? _loadTournamenCommand;
		//public DelegateCommand<string> LoadTournamentCommand =>
		//	_loadTournamenCommand ??= new DelegateCommand<string>(LoadTournament);

		public bool LoadTournament(string filePath)
		{
			Tournament? loadedTournament;

			using (TournamentDbContext tournamentDbContext = new TournamentDbContext(filePath))
			{
				
				loadedTournament = tournamentDbContext.Tournaments.FirstOrDefault();
			}

			if (loadedTournament == null)
			{
				return false;
			}

			_tournamentDatabase.Init(loadedTournament);

			return true;
		}
	}
}

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

namespace ZLDF.MainHost.ViewModels
{
	public class HostStartMenuViewModel : BindableBase
	{
		private IContainerRegistry _containerRegistry;

		public HostStartMenuViewModel(IContainerRegistry containerRegistry)
		{
			_containerRegistry = containerRegistry;
		}

		public string NewTournamentTitle { get; set; } = "New Tournament";

		private DelegateCommand<string>? _createTournamenCommand;
		public DelegateCommand<string> CreateTournamentCommand =>
			_createTournamenCommand ??= new DelegateCommand<string>(CreateTournament);
		public void CreateTournament(string filePath)
		{
			Tournament createdTournament = new Tournament();
			createdTournament.Title = NewTournamentTitle;

			using (TournamentDbContext tournamentDbContext = new TournamentDbContext(filePath))
			{
				tournamentDbContext.Database.EnsureCreated();
				tournamentDbContext.Add(createdTournament);
				tournamentDbContext.SaveChanges();
			}

			TestTournamentDatabase tournamentDb = new TestTournamentDatabase(createdTournament);
			_containerRegistry.RegisterInstance<ITournamentDatabase>(tournamentDb);
		}

		private DelegateCommand<string>? _loadTournamenCommand;
		public DelegateCommand<string> LoadTournamentCommand =>
			_loadTournamenCommand ??= new DelegateCommand<string>(LoadTournament);

		public void LoadTournament(string filePath)
		{

		}
	}
}

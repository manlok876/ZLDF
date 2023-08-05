using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using ZLDF.Core;
using ZLDF.DataAccess;

namespace ZLDF.MainHost.ViewModels
{
	public class TournamentViewModel : BindableBase
	{
		private readonly ITournamentDatabase _tournamentDb;

		public TournamentViewModel(ITournamentDatabase tournamentDb)
		{
			_tournamentDb = tournamentDb;
		}

		public Tournament Tournament => _tournamentDb.TournamentObject;
	}
}

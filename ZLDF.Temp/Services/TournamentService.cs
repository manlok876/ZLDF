using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Core;
using ZLDF.DataAccess;
using ZLDF.Temp.EF;

namespace ZLDF.Temp.Services
{
	public class TournamentService : ITournamentService
	{
		private readonly ITournamentDatabase _tournamentDatabase;

		public TournamentService(ITournamentDatabase tournamentDatabase)
		{
			_tournamentDatabase = tournamentDatabase;
		}

		public Tournament Tournament
		{
			get;
			private set;
		}

		public IEnumerable<Nomination> Nominations => Tournament.Nominations;

		public Tournament CreateNewTournament()
		{
			Tournament = new Tournament();

			_tournamentDatabase.SetTournament(Tournament);

			return Tournament;
		}

		public Tournament LoadTournament()
		{
			Tournament? loadedTournament = _tournamentDatabase.LoadTournament();

			if (loadedTournament == null)
			{
				CreateNewTournament();
			}
			else
			{
				Tournament = loadedTournament;
			}

			return Tournament;
		}

		public void SetTournamentTitle(string newTitle)
		{
			Tournament.Title = newTitle;
			_tournamentDatabase.SaveTournament();
		}
	}
}

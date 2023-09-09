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
	public class TestTournamentDatabase : ITournamentDatabase
	{
		private readonly IDatabaseService _database;
		public DatabaseReference DbReference => _database.DbReference;

		public TestTournamentDatabase(IDatabaseService databaseService)
		{
			_database = databaseService;
		}

		private Tournament? _tournament;
		public Tournament? TournamentObject => _tournament;

		public void SetTournament(Tournament tournament)
		{
			_tournament = tournament;
			using (TournamentDbContext tournamentDbContext =
				new TournamentDbContext(DbReference))
			{
				// Wipe, if needed we should ask for overwrite in dialog
				tournamentDbContext.Database.EnsureDeleted();
				tournamentDbContext.Database.EnsureCreated();
				tournamentDbContext.Add(tournament);
				tournamentDbContext.SaveChanges();
			}
		}

		public Tournament? LoadTournament()
		{
			if (_tournament == null)
			{
				using (TournamentDbContext tournamentDbContext =
					new TournamentDbContext(DbReference))
				{
					_tournament = tournamentDbContext.Tournaments.FirstOrDefault();
				}
			}

			return TournamentObject;
		}

		public void SaveTournament()
		{
			if (_tournament == null)
			{
				return;
			}
			using (TournamentDbContext tournamentDbContext =
				new TournamentDbContext(DbReference))
			{
				tournamentDbContext.Update(_tournament);
				tournamentDbContext.SaveChanges();
			}
		}
	}
}

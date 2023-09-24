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
			using (TournamentDbContext dbContext = new
				TournamentDbContext(DbReference))
			{
				// Wipe, if needed we should ask for overwrite in dialog
				dbContext.Database.EnsureDeleted();
				dbContext.Database.EnsureCreated();
				dbContext.Add(tournament);
				dbContext.SaveChanges();
			}
		}

		public Tournament? LoadTournament()
		{
			if (_tournament == null)
			{
				using (TournamentDbContext dbContext =
					new TournamentDbContext(DbReference))
				{
					// TODO: handle null
					_tournament = dbContext.Tournaments.FirstOrDefault();
					// TODO: how about we don't load everything?
					dbContext.Entry(_tournament!).Collection(t => t.Participants).Load();
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
			using (TournamentDbContext dbContext =
				new TournamentDbContext(DbReference))
			{
				dbContext.Update(_tournament);
				dbContext.SaveChanges();
			}
		}
	}
}

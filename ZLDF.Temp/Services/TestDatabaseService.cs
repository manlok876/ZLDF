using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Core;
using ZLDF.DataAccess;
using ZLDF.Temp.EF;

namespace ZLDF.Temp.Services
{
	public class TestDatabaseService : IDatabaseService
	{
		private DatabaseReference? _dbReference;
		public DatabaseReference DbReference =>
			_dbReference ?? throw new NullReferenceException("TournamentDatabase not connected");

		public void ConnectToDatabase(DatabaseReference dbReference)
		{
			_dbReference = dbReference;

			using (TournamentDbContext tournamentDbContext =
				new TournamentDbContext(DbReference))
			{
				tournamentDbContext.Database.EnsureCreated();
				tournamentDbContext.SaveChanges();
			}
		}
	}
}

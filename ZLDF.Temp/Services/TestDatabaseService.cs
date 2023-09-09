using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Core;
using ZLDF.DataAccess;
using ZLDF.DataAccess.EF;
using ZLDF.Temp.EF;

namespace ZLDF.Temp.Services
{
	public class TestDatabaseService : IDatabaseService
	{
		private DatabaseReference? _dbReference;
		public DatabaseReference DbReference =>
			_dbReference ?? throw new NullReferenceException("Database not connected");

		public void ConnectToDatabase(DatabaseReference dbReference)
		{
			_dbReference = dbReference;

			using (BaseDbContext dbContext =
				new BaseDbContext(DbReference))
			{
				dbContext.Database.EnsureCreated();
				dbContext.SaveChanges();
			}
		}
	}
}

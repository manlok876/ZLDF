using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ZLDF.Core;

namespace ZLDF.DataAccess.EF
{
	public class BaseDbContext : DbContext
	{
		public DatabaseReference DbReference { get; private set; }

		public BaseDbContext(DatabaseReference databaseReference)
		{
			DbReference = databaseReference;
		}
		protected string ConnectionString => GetConnectionString(DbReference);

		// Default constructor assumes url is a SQLite filepath
		public BaseDbContext(string url)
		{
			DbReference = new DatabaseReference(url);
			DbReference.ConnectionType = DatabaseType.SQLite;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			switch (DbReference.ConnectionType)
			{
				case DatabaseType.SQLite:
					optionsBuilder.UseSqlite(ConnectionString);
					break;
				default:
					break;
			}
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		public static string GetConnectionString(DatabaseReference databaseReference)
		{
			string result = $"Data Source={databaseReference.URL}";
			return result;
		}
	}
}

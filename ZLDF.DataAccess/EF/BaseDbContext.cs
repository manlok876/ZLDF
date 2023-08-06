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
		public DatabaseReference DatabaseReference { get; private set; }

		protected string ConnectionString => GetConnectionString(DatabaseReference);

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(ConnectionString);
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

		public BaseDbContext(DatabaseReference databaseReference)
		{
			DatabaseReference = databaseReference;
		}

		// Default constructor assumes url is a SQLite filepath
		public BaseDbContext(string url)
		{
			DatabaseReference = new DatabaseReference(url);
			DatabaseReference.ConnectionType = DatabaseType.SQLite;
		}
	}
}

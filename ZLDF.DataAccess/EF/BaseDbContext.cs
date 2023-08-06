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
		public string ConnectionString { get; private set; }

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
			ConnectionString = GetConnectionString(databaseReference);
		}

		public BaseDbContext(string url)
		{
			ConnectionString = $"Data Source={url}";
		}
	}
}

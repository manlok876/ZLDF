using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media.Media3D;

using Microsoft.EntityFrameworkCore;

using ZLDF.Core;
using ZLDF.DataAccess;

namespace ZLDF.Temp.EF
{
	public class TournamentDbContext : DbContext
	{
		public string ConnectionString { get; private set; }

		public DbSet<Tournament> Tournaments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(ConnectionString);
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		public static string GetConnectionString(DatabaseReference dbReference)
		{
			string result = $"Data Source={dbReference.URL}";
			return result;
		}

		public TournamentDbContext(DatabaseReference dbReference)
		{
			ConnectionString = GetConnectionString(dbReference);
		}

		public TournamentDbContext(string url)
		{
			ConnectionString = $"Data Source={url}";
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Media.Media3D;

using Microsoft.EntityFrameworkCore;

using ZLDF.Core;

namespace ZLDF.Temp.EF
{
	public class TournamentDbContext : DbContext
	{
		public string ConnectionString { get; private set; }

		internal DbSet<Tournament> Tournaments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(ConnectionString);
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
		public static string GetConnectionString(TournamentConnection tournamentConnection)
		{
			string result = $"Data Source={tournamentConnection.URL}";
			return result;
		}

		public TournamentDbContext(TournamentConnection tournamentConnection)
		{
			ConnectionString = GetConnectionString(tournamentConnection);
		}

		public TournamentDbContext(string url)
		{
			ConnectionString = $"Data Source={url}";
		}
	}
}

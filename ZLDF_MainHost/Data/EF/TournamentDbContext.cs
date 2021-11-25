using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZLDF.Classes;

namespace ZLDF.MainHost.Data.EF
{
	internal class TournamentDbContext : DbContext
	{
		public string ConnectionString { get; private set; }

		internal DbSet<Tournament> Tournaments { get; set; }
		internal DbSet<Fighter> Fighters { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite(ConnectionString);
			base.OnConfiguring(optionsBuilder);
		}

		public string GetConnectionString(TournamentConnection tournamentConnection)
		{
			string result = "";
			result = $"Data Source={tournamentConnection.URL}";
			return result;
		}

		public TournamentDbContext(TournamentConnection tournamentConnection)
		{
			ConnectionString = GetConnectionString(tournamentConnection);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using ZLDF.Core;
using ZLDF.DataAccess;
using ZLDF.DataAccess.EF;

namespace ZLDF.Temp.EF
{
	public class TournamentDbContext : BaseDbContext
	{
		public DbSet<Tournament> Tournaments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		public TournamentDbContext(DatabaseReference dbReference) : base(dbReference)
		{
		}

		public TournamentDbContext(string url) : base(url)
		{
		}
	}
}

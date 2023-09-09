using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using ZLDF.Core;
using ZLDF.DataAccess;
using ZLDF.DataAccess.EF;

namespace ZLDF.Temp.EF
{
	internal class PeopleDbContext : BaseDbContext
	{
		public PeopleDbContext(DatabaseReference dbReference) : base(dbReference)
		{
		}

		public PeopleDbContext(string url) : base(url)
		{
		}

		public DbSet<Person> Participants { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}

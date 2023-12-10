using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZLDF.Core;

namespace ZLDF.DataAccess.EF
{
	public class FullDbContext : BaseDbContext
	{
		public FullDbContext(DatabaseReference dbReference) : base(dbReference)
		{
		}

		public FullDbContext(string url) : base(url)
		{
		}

		public DbSet<Tournament> Tournaments { get; set; }
		public DbSet<Person> People { get; set; }

	}
}

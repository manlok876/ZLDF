using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.MainHost.Data.EF;

namespace ZLDF.MainHost.Data
{
	internal enum TournamentConnectionType
	{
		SQLite
	}

	internal class TournamentConnection
	{
		public string Name { get; set; }

		public TournamentConnectionType ConnectionType { get; set; }

		public string URL { get; set; }

		internal TournamentConnection()
		{
			Name = "Some Tournament";
			ConnectionType = TournamentConnectionType.SQLite;
			URL = "";
		}

		internal TournamentConnection(string url)
		{
			URL = url;
			using (TournamentDbContext tournamentDbContext = new TournamentDbContext(this))
			{
				Name = tournamentDbContext.Tournaments.FirstOrDefault()?.Name ?? "Some Tournament";
			}
			ConnectionType = TournamentConnectionType.SQLite;
		}
	}
}

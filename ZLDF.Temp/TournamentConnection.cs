using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Temp.EF;

namespace ZLDF.Temp
{
	public enum TournamentConnectionType
	{
		SQLite
	}

	public class TournamentConnection
	{
		public string Title { get; set; }

		public TournamentConnectionType ConnectionType { get; set; }

		public string URL { get; set; }

		internal TournamentConnection()
		{
			Title = "Some Tournament";
			ConnectionType = TournamentConnectionType.SQLite;
			URL = "";
		}

		internal TournamentConnection(string url)
		{
			URL = url;
			using (TournamentDbContext tournamentDbContext = new TournamentDbContext(this))
			{
				Title = tournamentDbContext.Tournaments.FirstOrDefault()?.Title ?? "Some Tournament";
			}
			ConnectionType = TournamentConnectionType.SQLite;
		}
	}
}

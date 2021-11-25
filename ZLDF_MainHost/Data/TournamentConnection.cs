﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			Name = "Some Tournament";
			ConnectionType = TournamentConnectionType.SQLite;
			URL = url;
		}
	}
}

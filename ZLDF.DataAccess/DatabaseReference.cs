using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.DataAccess
{
	public enum DatabaseType
	{
		SQLite
	}

	public class DatabaseReference
	{
		public DatabaseType ConnectionType { get; set; }

		public string URL { get; set; }

		public DatabaseReference()
		{
			ConnectionType = DatabaseType.SQLite;
			URL = "";
		}

		public DatabaseReference(string url)
		{
			URL = url;
			ConnectionType = DatabaseType.SQLite;
		}
	}
}

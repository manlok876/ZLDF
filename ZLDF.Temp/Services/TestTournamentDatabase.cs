using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Core;
using ZLDF.DataAccess;

namespace ZLDF.Temp.Services
{
	public class TestTournamentDatabase : ITournamentDatabase
	{
		private Tournament _tournament;
		public Tournament TournamentObject => _tournament;

		public void Init(Tournament tournament)
		{
			_tournament = tournament;
		}
	}
}

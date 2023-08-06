using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZLDF.Core;

namespace ZLDF.DataAccess
{
	public interface ITournamentService
	{
		public Tournament Tournament { get; }
		public IEnumerable<Nomination> Nominations { get; }

		public Tournament CreateNewTournament();
		public Tournament LoadTournament();
		public void SetTournamentTitle(string newTitle);
	}
}

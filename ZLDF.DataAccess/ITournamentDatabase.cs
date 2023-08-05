using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Core;

namespace ZLDF.DataAccess
{
	public interface ITournamentDatabase
	{
		public Tournament TournamentObject { get; }
	}
}

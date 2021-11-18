using System;
using System.Collections.Generic;
using System.Text;

namespace ZLDF.Classes
{
	public class Tournament : Event
	{
		private Fighter[] _fighters;
		public Fighter[] Fighters
		{
			get { return _fighters; }
			set
			{
				SetProperty(ref _fighters, value);
			}
		}
		public Tournament()
		{
			Fighters = new Fighter[0];
		}
	}
}

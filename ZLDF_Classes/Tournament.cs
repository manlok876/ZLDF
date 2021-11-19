using System;
using System.Collections.Generic;
using System.Text;

namespace ZLDF.Classes
{
	public class Tournament : Event
	{
		private List<Fighter> _fighters;

		public List<Fighter> Fighters
		{
			get { return _fighters; }
			set
			{
				SetProperty(ref _fighters, value);
			}
		}

		public Tournament()
		{
			_fighters = new List<Fighter>();
		}
	}
}

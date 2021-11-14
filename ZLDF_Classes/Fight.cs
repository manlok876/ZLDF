using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public abstract class Fight : BindableBase
	{
		public string Id { get; set; }
		public abstract Fighter[] Fighters { get; }

		public abstract float GetFighterScore(Fighter TheFighter);

		// Judges?
	}
}

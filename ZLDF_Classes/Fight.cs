using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public abstract class Fight : Event
	{
		private string _id;

		/// <summary>
		/// Id property is used for persistent tracking of fights, especially between different runs
		/// </summary>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				SetProperty(ref _id, value);
			}
		}
		public abstract ImmutableArray<Fighter> Fighters { get; }
		public abstract float GetFighterScore(Fighter fighter);

		protected Fight()
		{
			_id = "noId";
		}
	}
}

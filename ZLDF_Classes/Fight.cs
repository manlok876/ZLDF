using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public enum FightState
	{
		NotStarted,
		Scheduled,
		InProgress,
		Paused,
		Finished,
		Cancelled
	}

	public abstract class Fight : BindableBase
	{
		private string _id;
		private FightState _state;

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
		public FightState State
		{
			get
			{
				return _state;
			}
			set
			{
				SetProperty(ref _state, value);
			}
		}

		public abstract ImmutableArray<Fighter> Fighters { get; }
		public abstract float GetFighterScore(Fighter fighter);

		protected Fight()
		{
			_id = "noId";
			_state = FightState.NotStarted;
		}
	}
}

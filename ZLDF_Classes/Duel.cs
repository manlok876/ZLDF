using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ZLDF.Classes
{
	public class Duel : Fight
	{
		private Fighter _firstFighter;
		private Fighter _secondFighter;

		/// <summary>
		/// First fighter in a duel. Immutable, if pairings changed - create new fights
		/// </summary>
		public Fighter FirstFighter
		{
			get
			{
				return _firstFighter;
			}
			set
			{
				SetProperty(ref _firstFighter, value);
			}
		}

		/// <summary>
		/// Second fighter in a duel. Immutable, if pairings changed - create new fights
		/// </summary>
		public Fighter SecondFighter
		{
			get
			{
				return _secondFighter;
			}
			set
			{
				SetProperty(ref _secondFighter, value);
			}
		}

		public override IEnumerable<Fighter> Fighters
		{
			get
			{
				return new ImmutableArray<Fighter> { FirstFighter, SecondFighter };
			}
		}

		#region Scores

		private float _firstFighterScore;
		private float _secondFighterScore;

		public float FirstFighterScore
		{
			get
			{
				return _firstFighterScore;
			}
			set
			{
				SetProperty(ref _firstFighterScore, value);
			}
		}
		public float SecondFighterScore
		{
			get
			{
				return _secondFighterScore;
			}
			set
			{
				SetProperty(ref _secondFighterScore, value);
			}
		}
		public override float GetFighterScore(Fighter? fighter)
		{
			if (fighter == null)
			{
				return 0.0f;
			}

			if (fighter == FirstFighter)
			{
				return FirstFighterScore;
			}
			else if (fighter == SecondFighter)
			{
				return SecondFighterScore;
			}
			else
			{
				return 0.0f;
			}
		}

		public override void AddFighterScore(Fighter? fighter, float deltaScore)
		{
			if (fighter == null)
			{
				return;
			}

			if (fighter == FirstFighter)
			{
				FirstFighterScore += deltaScore;
			}
			else if (fighter == SecondFighter)
			{
				SecondFighterScore += deltaScore;
			}
		}

		#endregion // Scores

		#region Time

		private TimeSpan _totalTime;
		public TimeSpan TotalTime
		{
			get
			{
				return _totalTime;
			}
			set
			{
				SetProperty(ref _totalTime, value);
			}
		}

		private TimeSpan _remainingTime;
		public TimeSpan RemainingTime
		{
			get
			{
				return _remainingTime;
			}
			set
			{
				SetProperty(ref _remainingTime, value);
			}
		}

		#endregion // Time

		public Duel()
		{
		}

		public void Init(Fighter firstFighter, Fighter secondFighter)
		{
			FirstFighter = firstFighter;
			SecondFighter = secondFighter;

			FirstFighterScore = 0;
			SecondFighterScore = 0;

			TotalTime = new TimeSpan(0, 1, 20);
			RemainingTime = TotalTime;
		}
	}
}

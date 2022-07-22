using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Prism.Mvvm;

namespace ZLDF.Classes
{
	public class CountdownTimer : BindableBase
	{
		private DispatcherTimer _internalTimer;

		private TimeSpan _totalTime;
		public TimeSpan TotalTime
		{
			get { return _totalTime; }
			set
			{
				SetProperty(ref _totalTime, value);
			}
		}

		private TimeSpan _remainingTime;
		public TimeSpan RemainingTime
		{
			get { return _remainingTime; }
			set
			{
				SetProperty(ref _remainingTime, value);
			}
		}

		public void Reset()
		{
			RemainingTime = TotalTime;
			LastTickTime = DateTime.Now;
		}

		private void HandleInternalTimerTick(object? sender, EventArgs e)
		{
			if (!IsEnabled)
			{
				_internalTimer.Stop();
				return;
			}

			PerformTick();
		}

		private DateTime _lastTickTime;
		protected DateTime LastTickTime
		{
			get
			{
				return _lastTickTime;
			}
			private set
			{
				_lastTickTime = value;
			}
		}
		private void PerformTick()
		{
			if (!IsEnabled)
			{
				return;
			}

			DateTime now = DateTime.Now;
			if (LastTickTime > now)
			{
				// TODO: handle error
				return;
			}

			RemainingTime -= now - LastTickTime;
			LastTickTime = now;

			Tick?.Invoke(this, new EventArgs());
		}

		public event EventHandler? Tick;

		public void Start()
		{
			if (IsEnabled)
			{
				return;
			}

			if (!HasTimeRemaining)
			{
				return;
			}

			LastTickTime = DateTime.Now;
			_internalTimer.Start();
		}

		public void Stop()
		{
			if (!IsEnabled)
			{
				return;
			}

			_internalTimer.Stop();
			PerformTick();
		}

		public bool IsEnabled
		{
			get
			{
				return _internalTimer.IsEnabled;
			}
		}

		public bool HasTimeRemaining
		{
			get
			{
				return RemainingTime > TimeSpan.Zero;
			}
		}

		public CountdownTimer(TimeSpan totalTime, double tickRate)
		{
			_internalTimer = new DispatcherTimer();
			_internalTimer.Tick += HandleInternalTimerTick;

			_internalTimer.Interval = TimeSpan.FromSeconds(tickRate);

			TotalTime = totalTime;
			Reset();
		}

		public CountdownTimer() : this(new TimeSpan(0, 1, 0), 0.1)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.Classes;
using ZLDF.Scoreboard.Scoreboard.Views;
using ZLDF.Scoreboard.Scoreboard.ViewModels;

namespace ZLDF.Scoreboard.FightOperator.ViewModels
{
	internal class FightOperatorViewModel : BindableBase
	{
		#region Fights

		private Duel _currentDuel;
		public Duel CurrentDuel
		{
			get
			{
				return _currentDuel;
			}
			private set
			{
				if (_currentDuel is not null)
				{
					_currentDuel.PropertyChanged -= ScoreChangedListener;
				}

				InitTimerFromDuel(value);

				SetProperty(ref _currentDuel!, value);

				_currentDuel!.PropertyChanged += ScoreChangedListener;
			}
		}

		public List<Duel> _duels = new List<Duel>();
		public IEnumerable<Duel> AllDuels
		{
			get
			{
				return new List<Duel>(_duels);
			}
		}

		static public Duel CreateEmptyDuel()
		{
			Duel dummyDuel = new Duel();
			dummyDuel.Init(new Fighter(), new Fighter());
			return dummyDuel;
		}

		protected void AddDuelToList(Duel newDuel)
		{
			if (_duels.Contains(newDuel))
			{
				return;
			}
			_duels.Add(newDuel);
			newDuel.State = EventState.Scheduled;
			RaisePropertyChanged(nameof(AllDuels));
		}

		protected void ClearFightsList()
		{
			_duels.Clear();
			RaisePropertyChanged(nameof(AllDuels));
		}

		public ICommand MoveToNextFightCommand { get; private set; }
		public void MoveToNextFight()
		{
			if (_duels.Count < 1)
			{
				AddDuelToList(CreateEmptyDuel());
			}

			if (CurrentDuel is null)
			{
				MoveToFight(_duels[0]);
				return;
			}

			int nextFightIdx = _duels.IndexOf(CurrentDuel) + 1;
			if (nextFightIdx == -1 || nextFightIdx >= _duels.Count)
			{
				nextFightIdx = 0;
			}

			MoveToFight(_duels[nextFightIdx]);
		}

		public ICommand MoveToFightCommand { get; private set; }
		public void MoveToFight(Duel targetFight)
		{
			if (!_duels.Contains(targetFight))
			{
				// TODO: log warning?
				return;
			}
			if (!CurrentDuel?.IsOver ?? false)
			{
				CurrentDuel!.State = EventState.Paused;
			}
			// TODO: log fight changed event
			CurrentDuel = targetFight;
		}
		public ICommand RestartFightCommand { get; private set; }
		public void RestartFight()
		{
			//w.leftScore = w.rightScore = w.doubleHits = 0;
			//w.timeLeft = w.currentGamemode.TotalTime;
			//w.timer.Stop();
			//w.TimerTextBlock.Background = Brushes.Transparent;
			//w.matchInProgress = true;
			//w.UpdateScore();
			//w.UpdateTimer();
			//w.UpdateDoubleHits();

			// Reset time to max and score to zero
		}
		public ICommand FinishFightCommand { get; private set; }
		public void FinishFight()
		{
			StopFightTimer();
			CurrentDuel.State = EventState.Finished;
			AddDuelToList(CreateEmptyDuel());
			MoveToNextFight();
		}

		#endregion // Fights

		public Fighter FirstFighter
		{
			get
			{
				return CurrentDuel.FirstFighter;
			}
		}
		public Fighter SecondFighter
		{
			get
			{
				return CurrentDuel.SecondFighter;
			}
		}

		#region Scoreboard

		private ScoreboardViewModel? _scoreboardVM;
		private ScoreboardView? _scoreboardWindow;

		public ICommand OpenScoreboardCommand { get; private set; }
		public void OpenScoreboard()
		{
			if (_scoreboardWindow != null)
			{
				_scoreboardWindow.Close();
				_scoreboardWindow = null;
			}

			if (_scoreboardVM == null)
			{
				_scoreboardVM = new ScoreboardViewModel(this);
			}

			_scoreboardWindow = new ScoreboardView();
			_scoreboardWindow.DataContext = _scoreboardVM;
			_scoreboardWindow.Show();
		}

		public ICommand MaximizeScoreboardCommand { get; private set; }
		public void MaximizeScoreboard()
		{
			if (_scoreboardWindow != null)
			{
				_scoreboardWindow.WindowState = System.Windows.WindowState.Maximized;
			}
		}

		#endregion // Scoreboard

		#region Scores

		public float FirstFighterScore
		{
			get
			{
				return CurrentDuel.FirstFighterScore;
			}
		}
		public float SecondFighterScore
		{
			get
			{
				return CurrentDuel.SecondFighterScore;
			}
		}

		internal void ScoreChangedListener(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(CurrentDuel.FirstFighterScore))
			{
				RaisePropertyChanged(nameof(FirstFighterScore));
			}
			else if (e.PropertyName == nameof(CurrentDuel.SecondFighterScore))
			{
				RaisePropertyChanged(nameof(SecondFighterScore));
			}
		}

		public ICommand IncreaseFighterScoreCommand { get; private set; }
		public void IncreaseFighterScore(Fighter fighter)
		{
			CurrentDuel.AddFighterScore(fighter, 1);
		}

		public ICommand DecreaseFighterScoreCommand { get; private set; }
		public void DecreaseFighterScore(Fighter fighter)
		{
			CurrentDuel.AddFighterScore(fighter, -1);
		}
		protected void ResetScore()
		{
			CurrentDuel.FirstFighterScore = 0;
			CurrentDuel.SecondFighterScore = 0;
		}

		#endregion // Scores

		#region Time

		private CountdownTimer _fightTimer;
		public CountdownTimer FightTimer
		{
			get { return _fightTimer; }
			private set
			{
				SetProperty(ref _fightTimer, value);
			}
		}

		private void FightTick(object? sender, EventArgs e)
		{
			// ???
		}

		protected void InitTimerFromDuel(Duel duel)
		{
			StopFightTimer();
			FightTimer.TotalTime = duel.TotalTime;
			ResetTime();
		}

		public ICommand StartStopCommand { get; private set; }
		public void StartStop()
		{
			if (FightTimer.IsEnabled)
			{
				StopFightTimer();
			}
			else
			{
				StartFightTimer();
			}
		}

		public void StartFightTimer()
		{
			if (FightTimer.IsEnabled)
			{
				return;
			}

			if (!CurrentDuel.HasStarted)
			{
				CurrentDuel.State = EventState.InProgress;
			}
			// TODO: check if we can continue fight
			FightTimer.Start();
		}

		public void StopFightTimer()
		{
			if (!FightTimer.IsEnabled)
			{
				return;
			}
			FightTimer.Stop();
		}

		public ICommand ResetTimeCommand { get; private set; }
		private void ResetTime()
		{
			FightTimer.Stop();
			FightTimer.Reset();
		}

		#endregion // Time

		#region Colors

		private Color _firstFighterColor;
		public Color FirstFighterColor
		{
			get
			{
				return _firstFighterColor;
			}
			set
			{
				SetProperty(ref _firstFighterColor, value);
			}
		}

		private Color _secondFighterColor;
		public Color SecondFighterColor
		{
			get
			{
				return _secondFighterColor;
			}
			set
			{
				SetProperty(ref _secondFighterColor, value);
			}
		}

		public Color GetColorForFighter(Fighter? fighter)
		{
			if (fighter == FirstFighter)
			{
				return FirstFighterColor;
			}
			else if (fighter == SecondFighter)
			{
				return SecondFighterColor;
			}
			else
			{
				return Colors.White;
			}
		}

		#endregion Colors

		#region Sound

		public ICommand SetFightEndSoundCommand { get; private set; }
		public void SetFightEndSound(string filePath)
		{
			//w.sound = new SoundPlayer((String)filePath);
			//w.sound.Load();
		}

		#endregion // Sound

		public ICommand ExitAppCommand { get; private set; }
		public void ExitApp()
		{
			if (_scoreboardWindow != null)
			{
				_scoreboardWindow.Close();
			}
			//w.Close();
		}

		public FightOperatorViewModel()
		{
			FirstFighterColor = Colors.Red;
			SecondFighterColor = Colors.Blue;

			_fightTimer = new CountdownTimer();
			_fightTimer.TickRate = 0.01;

			AddDuelToList(CreateEmptyDuel());
			MoveToNextFight();

			OpenScoreboardCommand = new DelegateCommand(OpenScoreboard);
			MaximizeScoreboardCommand = new DelegateCommand(MaximizeScoreboard);

			IncreaseFighterScoreCommand = new DelegateCommand<Fighter>(IncreaseFighterScore);
			DecreaseFighterScoreCommand = new DelegateCommand<Fighter>(DecreaseFighterScore);

			StartStopCommand = new DelegateCommand(StartStop);
			ResetTimeCommand = new DelegateCommand(ResetTime);
			FinishFightCommand = new DelegateCommand(FinishFight);
		}
	}
}

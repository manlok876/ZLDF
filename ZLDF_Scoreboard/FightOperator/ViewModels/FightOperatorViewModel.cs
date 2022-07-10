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
		private Duel _currentDuel;
		public Duel CurrentDuel
		{
			get
			{
				return _currentDuel;
			}
			private set
			{
				_currentDuel.PropertyChanged -= ScoreChangedListener;

				InitTimerFromDuel(value);

				SetProperty(ref _currentDuel, value);

				_currentDuel.PropertyChanged += ScoreChangedListener;
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

		#region Fights

		public Duel CreateEmptyDuel()
		{
			Duel dummyDuel = new Duel();
			dummyDuel.Init(new Fighter(), new Fighter());
			return dummyDuel;
		}

		public ICommand MoveToNextFightCommand { get; private set; }
		public void MoveToNextFight()
		{
			// Find nextFight
			// MoveToFight(nextFight)
			throw new NotImplementedException();
		}

		public ICommand MoveToFightCommand { get; private set; }
		public void MoveToFight(Duel targetFight)
		{
			// Return if targetFight not in AllFights?
			// Pause? current fight
			// Log accordingly
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
			// Set state to finished
			// Move to next fight
			MoveToFight(CreateEmptyDuel());
		}

		#endregion // Fights

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
		private void ResetScore()
		{
			// Reset score to zero
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
			_fightTimer.TotalTime = duel.TotalTime;
			_fightTimer.Reset();
		}
		
		public void StartFightTimer()
		{
			if (_fightTimer.IsEnabled)
			{
				return;
			}
			// TODO: check if we can continue fight
			_fightTimer.Start();
		}

		public void StopFightTimer()
		{
			if (!_fightTimer.IsEnabled)
			{
				return;
			}
			_fightTimer.Stop();
		}

		private void ResetTime()
		{
			_fightTimer.Stop();
			_fightTimer.Reset();
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

			_currentDuel = CreateEmptyDuel();
			_currentDuel.PropertyChanged += ScoreChangedListener;

			OpenScoreboardCommand = new DelegateCommand(OpenScoreboard);
			MaximizeScoreboardCommand = new DelegateCommand(MaximizeScoreboard);

			FinishFightCommand = new DelegateCommand(FinishFight);
			IncreaseFighterScoreCommand = new DelegateCommand<Fighter>(IncreaseFighterScore);
			DecreaseFighterScoreCommand = new DelegateCommand<Fighter>(DecreaseFighterScore);
		}
	}
}

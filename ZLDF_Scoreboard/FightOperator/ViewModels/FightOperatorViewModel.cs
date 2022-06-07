using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
		private ScoreboardViewModel? _scoreboardVM;
		private ScoreboardView? _scoreboardWindow;

		private Duel _currentFight;
		public Duel CurrentFight
		{
			get
			{
				return _currentFight;
			}
			private set
			{
				_currentFight.PropertyChanged -= ScoreChangedListener;
				SetProperty(ref _currentFight, value);
				_currentFight.PropertyChanged += ScoreChangedListener;
			}
		}

		public List<Duel> _fights = new List<Duel>();

		public IEnumerable<Duel> AllFights
		{
			get
			{
				return new List<Duel>(_fights);
			}
		}

		public Fighter FirstFighter
		{
			get
			{
				return CurrentFight.FirstFighter;
			}
		}
		public Fighter SecondFighter
		{
			get
			{
				return CurrentFight.SecondFighter;
			}
		}
		
		public float FirstFighterScore
		{
			get
			{
				return CurrentFight.FirstFighterScore;
			}
		}
		public float SecondFighterScore
		{
			get
			{
				return CurrentFight.SecondFighterScore;
			}
		}

		internal void ScoreChangedListener(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(CurrentFight.FirstFighterScore))
			{
				RaisePropertyChanged(nameof(FirstFighterScore));
			}
			else if (e.PropertyName == nameof(CurrentFight.SecondFighterScore))
			{
				RaisePropertyChanged(nameof(SecondFighterScore));
			}
		}

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

		#region Commands

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
			CurrentFight = targetFight;
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
		private void ResetScore()
		{
			// Reset score to zero
		}
		private void ResetTime()
		{
			// Reset time to max
		}

		public ICommand SetFightEndSoundCommand { get; private set; }
		public void SetFightEndSound(string filePath)
		{
			//w.sound = new SoundPlayer((String)filePath);
			//w.sound.Load();
		}

		public ICommand ExitAppCommand { get; private set; }
		public void ExitApp()
		{
			if (_scoreboardWindow != null)
			{
				_scoreboardWindow.Close();
			}
			//w.Close();
		}

		public ICommand AddPointsCommand { get; private set; }
		public void AddPoints()
		{
			if (CurrentFight == null)
			{
				return;
			}
			CurrentFight.FirstFighterScore += 1;
			Trace.WriteLine(CurrentFight.FirstFighterScore.ToString());
		}

		#endregion // Commands

		public FightOperatorViewModel()
		{
			FirstFighterColor = Colors.Red;
			SecondFighterColor = Colors.Blue;

			_currentFight = CreateEmptyDuel();
			_currentFight.PropertyChanged += ScoreChangedListener;

			OpenScoreboardCommand = new DelegateCommand(OpenScoreboard);
			MaximizeScoreboardCommand = new DelegateCommand(MaximizeScoreboard);

			FinishFightCommand = new DelegateCommand(FinishFight);
			AddPointsCommand = new DelegateCommand(AddPoints);
		}
	}
}

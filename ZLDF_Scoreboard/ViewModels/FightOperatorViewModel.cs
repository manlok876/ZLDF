using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.Scoreboard.Views;
using ZLDF.Classes;

namespace ZLDF.Scoreboard.ViewModels
{
	internal class FightOperatorViewModel : BindableBase
	{
		private ScoreboardViewModel _scoreboardVM;
		private ScoreboardView _scoreboardWindow;

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

		#region Commands

		public ICommand OpenScoreboardCommand { get; private set; }
		public void OpenScoreboard()
		{
			// If already open close it
			// If VM not created - create
			// Create view using that VM
			throw new NotImplementedException();
		}

		public ICommand MaximizeScoreboardCommand { get; private set; }
		public void MaximizeScoreboard()
		{
			//if (showWindow != null)
			//{
			//	showWindow.WindowState = System.Windows.WindowState.Maximized;
			//}
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
			//if (w.showWindow != null) w.showWindow.Close();
			//w.Close();
		}

		#endregion

		public FightOperatorViewModel()
		{
			_currentFight = CreateEmptyDuel();
			_currentFight.PropertyChanged += ScoreChangedListener;

		}
	}
}

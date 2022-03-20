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

		public Fight CurrentFight { get; private set; }

		public List<Fight> _fights;
		public IEnumerable<Fight> AllFights
		{
			get
			{
				return new List<Fight>(_fights);
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

		public ICommand MoveToNextFightCommand { get; private set; }
		public void MoveToNextFight()
		{
			// Find nextFight
			// MoveToFight(nextFight)
			throw new NotImplementedException();
		}

		public ICommand MoveToFightCommand { get; private set; }
		public void MoveToFight(Fight targetFight)
		{
			// Return if targetFight not in AllFights?
			// Pause? current fight
			// Log accordingly
			// CurrentFight = targetFight
			throw new NotImplementedException();
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

		}
	}
}

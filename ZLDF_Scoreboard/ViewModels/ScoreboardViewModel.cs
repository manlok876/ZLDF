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

namespace ZLDF.Scoreboard.ViewModels
{
	internal class ScoreboardViewModel : BindableBase
	{

		#region Commands
		public ICommand RestartCommand { get; private set; }
		public void Restart()
		{
			//w.leftScore = w.rightScore = w.doubleHits = 0;
			//w.timeLeft = w.currentGamemode.TotalTime;
			//w.timer.Stop();
			//w.TimerTextBlock.Background = Brushes.Transparent;
			//w.matchInProgress = true;
			//w.UpdateScore();
			//w.UpdateTimer();
			//w.UpdateDoubleHits();

		}

		public ICommand ShowSecondWindowCommand { get; private set; }
		public void ShowSecondWindow()
		{
			//if (w.showWindow != null)
			//{
			//	w.showWindow.Close();
			//}
			//w.showWindow = new ShowWindow
			//{
			//	Owner = w
			//};
			//w.showWindow.Show();
			//w.showWindow.UpdateColor();
			//w.showWindow.UpdateScore();
			//w.showWindow.UpdateTimer();
			//w.showWindow.UpdateDoubleHits();
		}

		public ICommand MaximizeSecondWindowCommand { get; private set; }
		public void MaximizeSecondWindow()
		{
			//if (showWindow != null)
			//{
			//	showWindow.WindowState = System.Windows.WindowState.Maximized;
			//}
		}

		public ICommand ChangeGamemodeCommand { get; private set; }
		public void ChangeGamemode(string gamemodeName)
		{
			//w.currentGamemode = w.gamemodes[(string)parameter];
			//w._restartCmd.Execute(null);
		}

		public ICommand ChangeSoundCommand { get; private set; }
		public void ChangeSound(string filePath)
		{
			//w.sound = new SoundPlayer((String)filePath);
			//w.sound.Load();
		}

		public ICommand ChangeArenaCommand { get; private set; }
		public void ChangeArena()
		{
			//w.arena = (Int32)parameter;
			//w.ArenaTextBlock.Text = String.Format("Ристалище {0}", w.arena);
			//if (w.showWindow != null)
			//{
			//	w.showWindow.ArenaTextBlock.Text = String.Format("Ристалище {0}", w.arena);
			//}
		}

		public ICommand ExitCommand { get; private set; }
		public void Exit()
		{
			//if (w.showWindow != null) w.showWindow.Close();
			//w.Close();
		}

		#endregion

		public ScoreboardViewModel()
		{

		}
	}
}

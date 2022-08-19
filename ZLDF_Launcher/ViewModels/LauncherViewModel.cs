using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using ZLDF.MainHost;
using ZLDF.Scoreboard;

namespace ZLDF.Launcher.ViewModels
{
	internal class LauncherViewModel
	{
		#region Apps

		public ICommand LaunchMainHostCommand { get; private set; }
		public void LaunchMainHost()
		{
			ZLDF.MainHost.App MainHostApp = new();
			MainHostApp.Run();
		}

		#endregion // Apps

		public LauncherViewModel()
		{
			LaunchMainHostCommand = new DelegateCommand(LaunchMainHost);
		}
	}
}

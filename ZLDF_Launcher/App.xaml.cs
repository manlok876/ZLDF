using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZLDF.Launcher.ViewModels;
using ZLDF.Launcher.Views;

namespace ZLDF.Launcher
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			LauncherView app = new();
			LauncherViewModel context = new();
			app.DataContext = context;
			app.Show();
		}
	}
}

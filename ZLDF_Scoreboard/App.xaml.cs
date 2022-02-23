using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZLDF.Scoreboard.ViewModels;
using ZLDF.Scoreboard.Views;

namespace ZLDF.Scoreboard
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			AppView app = new AppView();
			AppViewModel context = new AppViewModel();
			app.DataContext = context;
			app.Show();
		}
	}
}

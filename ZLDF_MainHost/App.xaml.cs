using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZLDF.MainHost.ViewModels;
using ZLDF.MainHost.Views;
using ZLDF.MainHost.Testing.ViewModels;
using ZLDF.MainHost.Testing.Views;

namespace ZLDF.MainHost
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			//MainTestingView test = new MainTestingView();
			//MainTestingViewModel testContext = new MainTestingViewModel();
			//test.DataContext = testContext;
			//test.Show();

			AppView app = new AppView();
			AppViewModel context = new AppViewModel();
			app.DataContext = context;
			app.Show();
		}
	}
}

using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using ZLDF.App.Views;
using ZLDF.MainHost;
using ZLDF.Scoreboard;
using ZLDF.Temp;

namespace ZLDF.App
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{

		}

		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			base.ConfigureModuleCatalog(moduleCatalog);

			moduleCatalog.AddModule<TempModule>();
			moduleCatalog.AddModule<MainHostModule>();
			moduleCatalog.AddModule<ScoreboardModule>();
		}
	}
}

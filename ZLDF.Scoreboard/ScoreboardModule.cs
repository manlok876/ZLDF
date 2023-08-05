using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ZLDF.Scoreboard.ViewModels;
using ZLDF.Scoreboard.Views;
using ZLDF.WPF;

namespace ZLDF.Scoreboard
{
	public class ScoreboardModule : IModule
	{
		private readonly IRegionManager _regionManager;

		public ScoreboardModule(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void OnInitialized(IContainerProvider containerProvider)
		{
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			_regionManager.RegisterViewWithRegion<ScoreboardView>(RegionNames.ShellRegion);


			//containerRegistry.RegisterForNavigation<ScoreboardView, ScoreboardViewModel>();
		}
	}
}
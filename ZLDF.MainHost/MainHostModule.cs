using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ZLDF.MainHost.ViewModels;
using ZLDF.MainHost.Views;
using ZLDF.WPF;

namespace ZLDF.MainHost
{
	public class MainHostModule : IModule
	{
		private readonly IRegionManager _regionManager;

		public MainHostModule(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void OnInitialized(IContainerProvider containerProvider)
		{
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			_regionManager.RegisterViewWithRegion<HostStartMenu>(RegionNames.MainHostRegion);
			_regionManager.RegisterViewWithRegion<MainHostView>(RegionNames.ShellRegion);

			//containerRegistry.RegisterForNavigation<MainHostView, MainHostViewModel>();
		}
	}
}
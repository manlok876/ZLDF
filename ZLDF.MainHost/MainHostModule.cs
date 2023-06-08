using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
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
			_regionManager.RegisterViewWithRegion<ViewA>(RegionNames.TestRegion);
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{

		}
	}
}
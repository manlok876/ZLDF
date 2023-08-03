using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ZLDF.DataAccess;

namespace ZLDF.Temp
{
	public class TempModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{

		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.Register<IPeopleDatabase, Services.TestPeopleDatabase>();
		}
	}
}
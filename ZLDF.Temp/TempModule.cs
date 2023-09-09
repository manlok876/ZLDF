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
			containerRegistry.RegisterSingleton<IDatabaseService, Services.TestDatabaseService>();
			containerRegistry.RegisterSingleton<ITournamentDatabase, Services.TestTournamentDatabase>();
			containerRegistry.RegisterSingleton<IPeopleDatabase, Services.TestPeopleDatabase>();
			containerRegistry.RegisterSingleton<ITournamentService, Services.TournamentService>();
		}
	}
}

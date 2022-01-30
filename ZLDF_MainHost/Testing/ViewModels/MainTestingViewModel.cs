using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.MainHost.Testing.Views;

namespace ZLDF.MainHost.Testing.ViewModels
{
	internal class MainTestingViewModel : BindableBase
	{
		public GroupsTestViewModel GroupsTestVM
		{
			get;
			private set;
		}
		public RoundRobinMMTestViewModel RoundRobinTestVM
		{
			get;
			private set;
		}

		public MainTestingViewModel()
		{
			GroupsTestVM = new GroupsTestViewModel();
			RoundRobinTestVM = new RoundRobinMMTestViewModel();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

using Prism.Mvvm;
using Prism.Commands;

namespace ZLDF.MainHost.ViewModels
{
	public class MainHostViewModel : BindableBase
	{
		public string Title => "Main Host";

		private string _message = "Main Host View";
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}

		public MainHostViewModel()
		{

		}
	}
}

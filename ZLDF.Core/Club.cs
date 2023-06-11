using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Core
{

	public class Club : BindableBase
	{
		public string Name { get; set; }

		public Club()
		{
			Name = "-???-";
		}
	}
}

using System;
using System.Text;
using Prism.Mvvm;

namespace ZLDF.Classes
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

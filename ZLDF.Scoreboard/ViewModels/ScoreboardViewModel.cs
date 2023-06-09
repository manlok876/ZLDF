using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLDF.Scoreboard.Views;

namespace ZLDF.Scoreboard.ViewModels
{
	internal class ScoreboardViewModel : BindableBase
	{
		public string Title => "Scoreboard";

		public string Message => "Scoreboard View";

		public ScoreboardViewModel()
		{
		}
	}
}

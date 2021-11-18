using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace ZLDF.MainHost.ViewModels
{
	internal class AppViewModel : BindableBase
	{
		private TournamentViewModel _selectedTournament;

		public TournamentViewModel SelectedTournament
		{
			get { return _selectedTournament; }
			set
			{
				SetProperty(ref _selectedTournament, value);
			}
		}

		public AppViewModel()
		{
			_selectedTournament = new TournamentViewModel();
		}

	}
}

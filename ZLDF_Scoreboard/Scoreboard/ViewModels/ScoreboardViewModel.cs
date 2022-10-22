using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.Classes;
using ZLDF.Scoreboard.FightOperator.ViewModels;

namespace ZLDF.Scoreboard.Scoreboard.ViewModels
{
	internal class ScoreboardViewModel : BindableBase
	{
		private readonly FightOperatorViewModel _operatorViewModel;
		public FightOperatorViewModel FightOperatorVM
		{
			get { return _operatorViewModel; }
		}

		#region Mirroring

		private bool _bIsMirrored = true;
		public bool IsMirrored
		{
			get { return _bIsMirrored; }
			set
			{
				SetProperty(ref _bIsMirrored, value);
				UpdateFlipState();
			}
		}

		public bool IsFlipped
		{
			get
			{
				// TODO: route proper logic
				return IsMirrored;
			}
		}

		internal void UpdateFlipState()
		{
			RaisePropertyChanged(nameof(IsFlipped));
		}

		#endregion // Mirroring

		#region FightersInfo

		private bool _displayFighterName = true;
		public bool DisplayFighterName
		{
			get
			{
				return _displayFighterName;
			}
			set
			{
				SetProperty(ref _displayFighterName, value);
				RaisePropertyChanged(nameof(DisplayFighterClub));
			}
		}

		private bool _displayFighterClub = true;
		public bool DisplayFighterClub
		{
			get
			{
				return DisplayFighterName && _displayFighterClub;
			}
			set
			{
				SetProperty(ref _displayFighterClub, value);
			}
		}

		#endregion // FightersInfo

		public ScoreboardViewModel(FightOperatorViewModel operatorViewModel)
		{
			_operatorViewModel = operatorViewModel;
		}
	}
}

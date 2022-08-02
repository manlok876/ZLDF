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

		public ScoreboardViewModel(FightOperatorViewModel operatorViewModel)
		{
			_operatorViewModel = operatorViewModel;
		}
	}
}

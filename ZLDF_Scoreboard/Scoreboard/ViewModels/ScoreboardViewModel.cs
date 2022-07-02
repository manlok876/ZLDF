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

		public bool AreFightersSwapped { get; set; }

		public int ArenaNumber { get; }

		public Duel CurrentFight
		{
			get
			{
				Duel? currentDuel = _operatorViewModel.CurrentDuel as Duel;
				return currentDuel ?? new Duel();
			}
		}

		public Fighter LeftFighter
		{
			get
			{
				if (AreFightersSwapped)
				{
					return CurrentFight.SecondFighter;
				}
				else
				{
					return CurrentFight.FirstFighter;
				}
			}
		}
		
		public Fighter RightFighter
		{
			get
			{
				if (AreFightersSwapped)
				{
					return CurrentFight.FirstFighter;
				}
				else
				{
					return CurrentFight.SecondFighter;
				}
			}
		}

		public int LeftScore
		{
			get
			{
				return (int)CurrentFight.GetFighterScore(LeftFighter);
			}
		}

		public int RightScore
		{
			get
			{
				return (int)CurrentFight.GetFighterScore(RightFighter);
			}
		}

		public ScoreboardViewModel(FightOperatorViewModel operatorViewModel)
		{
			_operatorViewModel = operatorViewModel;
		}
	}
}

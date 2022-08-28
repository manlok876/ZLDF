using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Prism.Mvvm;
using Prism.Commands;
using ZLDF.Classes;
using ZLDF.Scoreboard.FightOperator.ViewModels;

namespace ZLDF.Scoreboard.FightOperator.Views
{
	/// <summary>
	/// Interaction logic for FightOperator.xaml
	/// </summary>
	public partial class FightOperatorView : Window, INotifyPropertyChanged
	{
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler? PropertyChanged;
		protected void RaisePropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			PropertyChanged?.Invoke(this, args);
		}
		#endregion

		internal FightOperatorViewModel? ViewModel
		{
			get
			{
				return DataContext as FightOperatorViewModel;
			}
		}
		internal void SetViewModel(FightOperatorViewModel NewVM)
		{
			DataContext = NewVM;
			RaisePropertyChanged(nameof(ViewModel));
		}

		private bool bIsFlipped = false;
		public bool IsFlipped
		{
			get
			{
				return bIsFlipped;
			}
			private set
			{
				bIsFlipped = value;
				RaisePropertyChanged(nameof(IsFlipped));
			}
		}

		public ICommand SwapFightersCommand { get; private set; }
		void SwapFighters()
		{
			IsFlipped = !IsFlipped;
		}

		public Fighter GetLeftFighter()
		{
			return IsFlipped ? ViewModel.SecondFighter : ViewModel.FirstFighter;
		}
		
		public Fighter GetRightFighter()
		{
			return IsFlipped ? ViewModel.FirstFighter : ViewModel.SecondFighter;
		}

		public FightOperatorView()
		{
			InitializeComponent();

			SwapFightersCommand = new DelegateCommand(SwapFighters);
			
			KeyDown += new KeyEventHandler(OnButtonKeyDown);
		}

		private void FightsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ListView? lv = sender as ListView;
			Duel? d = lv?.SelectedItem as Duel;
			if (d != null)
			{
				ViewModel?.MoveToFight(d);
			}
		}

		#region Input

		/*
			F1,F2,F3 или Q,W,E - прибавить левому бойцу 1, 2, или 3 балла
			F4 или Z - снять 1 балл
			F12,F11,F10 или P,O,I - прибавить правому бойцу 1, 2, или 3 балла
			F9 или M - снять 1 балл

			F5 - отметить обоюдку
			F6 - убрать обоюдку
			F8 - перезапуск матча
			F7 или Y - последний сход

			Пробел - пауза

			F - прибавить 1 секунду
			G - вычесть 1 секунду
			J - добавить 1 минуту
			T - поменять местами цвета

			Все буквы латинские
		* */
		private void OnButtonKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				ViewModel?.StartStop();
			}
			else if (e.Key == Key.Q || e.Key == Key.F1)
			{
				ViewModel?.IncreaseFighterScore(GetLeftFighter());
			}
			//else if (e.Key == Key.W || e.Key == Key.F2)
			//{
			//	//leftScore += 2;
			//}
			//else if (e.Key == Key.E || e.Key == Key.F3)
			//{
			//	//leftScore += 3;
			//}
			else if (e.Key == Key.Z || e.Key == Key.F4)
			{
				ViewModel?.DecreaseFighterScore(GetLeftFighter());
			}
			//else if (e.Key == Key.I || e.Key == Key.System)
			//{
			//	//rightScore += 3;
			//}
			//else if (e.Key == Key.O || e.Key == Key.F11)
			//{
			//	//rightScore += 2;
			//}
			else if (e.Key == Key.P || e.Key == Key.F12)
			{
				ViewModel?.IncreaseFighterScore(GetRightFighter());
			}
			else if (e.Key == Key.M || e.Key == Key.F9)
			{
				ViewModel?.DecreaseFighterScore(GetRightFighter());
			}
			else if (e.Key == Key.T)
			{
				SwapFighters();
			}
			else if (e.Key == Key.F)
			{
				ViewModel?.IncreaseRemainingTime(new TimeSpan(0, 0, 1));
			}
			else if (e.Key == Key.G)
			{
				ViewModel?.DecreaseRemainingTime(new TimeSpan(0, 0, 1));
			}
			else if (e.Key == Key.J)
			{
				ViewModel?.IncreaseRemainingTime(new TimeSpan(0, 1, 0));
			}
			else if (e.Key == Key.F8)
			{
				ViewModel?.RestartFight();
			}
			//else if (e.Key == Key.F5)
			//{
			//	//doubleHits++;
			//	//UpdateDoubleHits();
			//}
			//else if (e.Key == Key.F6)
			//{
			//	//if (doubleHits > 0)
			//	//{
			//	//	doubleHits--;
			//	//	UpdateDoubleHits();
			//	//}
			//}
			//else if (e.Key == Key.Y || e.Key == Key.F7)
			//{
			//	//timeLeft = currentGamemode.FinalRoundTime;
			//	//TimerTextBlock.Background = Brushes.LightGreen;
			//	//matchInProgress = true;
			//	//UpdateTimer();
			//}
		}

		#endregion // Input
	}
}

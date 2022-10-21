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

		public Fighter GetLeftFighter()
		{
			return ViewModel.IsFlipped ? ViewModel.SecondFighter : ViewModel.FirstFighter;
		}
		
		public Fighter GetRightFighter()
		{
			return ViewModel.IsFlipped ? ViewModel.FirstFighter : ViewModel.SecondFighter;
		}

		public FightOperatorView()
		{
			InitializeComponent();

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
			F1 / W - прибавить левому бойцу 1 балл
			F4 / Q - снять 1 балл
			F12 / O - прибавить правому бойцу 1 балл
			F9 / P - снять 1 балл

			R - перезапуск матча
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
			else if (e.Key == Key.W || e.Key == Key.F1)
			{
				ViewModel?.IncreaseFighterScore(GetLeftFighter());
			}
			else if (e.Key == Key.Q || e.Key == Key.F4)
			{
				ViewModel?.DecreaseFighterScore(GetLeftFighter());
			}
			else if (e.Key == Key.O || e.Key == Key.F12)
			{
				ViewModel?.IncreaseFighterScore(GetRightFighter());
			}
			else if (e.Key == Key.P || e.Key == Key.F9)
			{
				ViewModel?.DecreaseFighterScore(GetRightFighter());
			}
			else if (e.Key == Key.T)
			{
				ViewModel?.SwapSides();
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
			else if (e.Key == Key.R)
			{
				ViewModel?.RestartFight();
			}
		}

		#endregion // Input
	}
}

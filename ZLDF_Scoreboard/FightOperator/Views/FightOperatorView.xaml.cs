﻿using System;
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
			if (ViewModel != null)
			{
				ViewModel.PropertyChanged -= VMChangesListener;
			}

			DataContext = NewVM;
			RaisePropertyChanged(nameof(ViewModel));

			NewVM.PropertyChanged += VMChangesListener;
		}

		public Duel? CurrentFight
		{
			get
			{
				return ViewModel?.CurrentDuel;
			}
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

		public Fighter? LeftFighter
		{
			get
			{
				if (IsFlipped)
				{
					return CurrentFight?.FirstFighter;
				}
				else
				{
					return CurrentFight?.SecondFighter;
				}
			}
		}
		public Fighter? RightFighter
		{
			get
			{
				if (IsFlipped)
				{
					return CurrentFight?.SecondFighter;
				}
				else
				{
					return CurrentFight?.FirstFighter;
				}
			}
		}

		public int LeftScore
		{
			get
			{
				return (int) (CurrentFight?.GetFighterScore(LeftFighter) ?? -1);
			}
		}
		public int RightScore
		{
			get
			{
				return (int) (CurrentFight?.GetFighterScore(RightFighter) ?? -1);
			}
		}

		public ICommand SwapFightersCommand { get; private set; }
		void SwapFighters()
		{
			IsFlipped = !IsFlipped;
		}

		private void VMChangesListener(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(ViewModel.CurrentDuel))
			{
				RaisePropertyChanged(nameof(CurrentFight));
			}
			Trace.WriteLine("VM " + e.PropertyName + " changed");
		}

		private void HandlePropertyUpdated(object? sender, PropertyChangedEventArgs e)
		{
			//if (e.PropertyName == nameof(CurrentFight))
			//{
			//	RaisePropertyChanged(nameof(LeftFighter));
			//	RaisePropertyChanged(nameof(RightFighter));
			//}
			//Trace.WriteLine(e.PropertyName + " changed");
		}

		public FightOperatorView()
		{
			InitializeComponent();

			PropertyChanged += HandlePropertyUpdated;

			SwapFightersCommand = new DelegateCommand(SwapFighters);
		}
	}
}

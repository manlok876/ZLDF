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

		public FightOperatorView()
		{
			InitializeComponent();

			SwapFightersCommand = new DelegateCommand(SwapFighters);
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
	}
}

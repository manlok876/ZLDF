﻿using System.Windows.Controls;
using ZLDF.DataAccess;
using ZLDF.MainHost.ViewModels;

namespace ZLDF.MainHost.Views
{
	/// <summary>
	/// Interaction logic for ParticipantsView
	/// </summary>
	public partial class ParticipantsView : UserControl
	{
		private ParticipantsViewModel? ViewModel => DataContext as ParticipantsViewModel;

		public ParticipantsView()
		{
			InitializeComponent();
		}

		private void HandlePersonDataChanged(object sender, System.Windows.RoutedEventArgs e)
		{
			ViewModel?.UpdateFighter();
		}
	}
}

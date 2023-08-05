using Microsoft.Win32;
using Prism.Regions;
using System.Windows.Controls;
using ZLDF.MainHost.ViewModels;
using ZLDF.WPF;

namespace ZLDF.MainHost.Views
{
	/// <summary>
	/// Interaction logic for PrismUserControl1
	/// </summary>
	public partial class HostStartMenu : UserControl
	{
		private readonly IRegionManager _regionManager;

		public HostStartMenu(IRegionManager regionManager)
		{
			InitializeComponent();

			_regionManager = regionManager;
		}

		private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.OverwritePrompt = true;

			if (dialog.ShowDialog() == true)
			{
				string destinationFilePath = dialog.FileName;
				(DataContext as HostStartMenuViewModel)?.CreateTournament(destinationFilePath);
				_regionManager.RequestNavigate(RegionNames.MainHostRegion, "TournamentView");
			}
		}

		private void LoadButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();

			if (dialog.ShowDialog() == true)
			{
				string destinationFilePath = dialog.FileName;
				bool? result = (DataContext as HostStartMenuViewModel)?.LoadTournament(destinationFilePath);

				if (result ?? false)
				{
					_regionManager.RequestNavigate(RegionNames.MainHostRegion, "TournamentView");
				}
			}
		}
	}
}

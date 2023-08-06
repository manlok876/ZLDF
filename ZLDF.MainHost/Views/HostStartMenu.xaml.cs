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
		public HostStartMenu()
		{
			InitializeComponent();
		}

		private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.OverwritePrompt = true;

			if (dialog.ShowDialog() == true)
			{
				string destinationFilePath = dialog.FileName;
				(DataContext as HostStartMenuViewModel)?.CreateTournament(destinationFilePath);
			}
		}

		private void LoadButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();

			if (dialog.ShowDialog() == true)
			{
				string destinationFilePath = dialog.FileName;
				(DataContext as HostStartMenuViewModel)?.LoadTournament(destinationFilePath);
			}
		}
	}
}

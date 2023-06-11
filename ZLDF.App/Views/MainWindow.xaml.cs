using Prism.Regions;
using System.Windows;
using ZLDF.WPF;

namespace ZLDF.App.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly IRegionManager _regionManager;

		public MainWindow(IRegionManager regionManager)
		{
			InitializeComponent();

			_regionManager = regionManager;
		}

		private void MainHostButton_Click(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.TestRegion, "MainHostView");
		}

		private void ScoreboardButton_Click(object sender, RoutedEventArgs e)
		{
			_regionManager.RequestNavigate(RegionNames.TestRegion, "ScoreboardView");
		}
	}
}

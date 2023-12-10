using System.Windows.Controls;
using ZLDF.MainHost.ViewModels;
using ZLDF.Core;

namespace ZLDF.MainHost.Views
{
	/// <summary>
	/// Interaction logic for PrismUserControl1
	/// </summary>
	public partial class TournamentView : UserControl
	{
		public TournamentView()
		{
			InitializeComponent();
		}

		private void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// Show nomination creation dialogue
			if (false /* check dialog results */)
			{
				// Check if nomination exists
				(DataContext as TournamentViewModel)?.AddNomination(new Nomination());
			}
		}

		private void RemoveButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{

		}
	}
}

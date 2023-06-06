using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZLDF.MainHost.Views
{
	/// <summary>
	/// Interaction logic for CreateTournamentDialog.xaml
	/// </summary>
	public partial class CreateTournamentDialog : Window
	{
		public Tournament Tournament
		{
			get;
			private set;
		}

		public CreateTournamentDialog()
		{
			Tournament = TestData.GenerateTestTournament(5, 3);
			DataContext = this;
			InitializeComponent();
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}

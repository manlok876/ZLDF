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
using ZLDF.Classes;

namespace ZLDF.Scoreboard.Views
{
	/// <summary>
	/// Interaction logic for ExportFightsTSVDialog.xaml
	/// </summary>
	public partial class ExportFightsTSVDialog : Window
	{
		private IEnumerable<Duel> _duels;

		public ExportFightsTSVDialog(IEnumerable<Duel> duels)
		{
			InitializeComponent();

			_duels = duels;

			DuelsTextBox.Text = ZLDFUtils.GetTSVFromDuels(duels);
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}

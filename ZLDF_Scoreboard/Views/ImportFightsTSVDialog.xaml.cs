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
	/// Interaction logic for ImportFightsTSVDialog.xaml
	/// </summary>
	public partial class ImportFightsTSVDialog : Window
	{
		public ImportFightsTSVDialog()
		{
			InitializeComponent();
		}

		private List<Duel> _duels = new();
		public IEnumerable<Duel> Duels
		{
			get
			{
				return _duels;
			}
			private set
			{
				_duels = new List<Duel>(value);
			}
		}

		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			Duels = ZLDFUtils.ParseDuelsFromTSVString(DuelsTextBox.Text);

			DialogResult = true;
		}
	}
}

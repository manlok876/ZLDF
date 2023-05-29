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
using NetTest.ViewModels;

namespace NetTest.Views
{
	/// <summary>
	/// Interaction logic for HttpReceiveView.xaml
	/// </summary>
	public partial class HttpReceiveView : Window
	{
		public HttpReceiveView()
		{
			InitializeComponent();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (DataContext is HttpReceiveViewModel vm)
			{
				vm.HandleClosing();
			}
		}
	}
}

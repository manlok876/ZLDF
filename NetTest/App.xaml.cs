using NetTest.ViewModels;
using NetTest.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NetTest
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			//NetTestView app = new NetTestView();
			//NetTestViewModel context = new NetTestViewModel();
			//app.DataContext = context;
			//app.Show();

			HttpSendView client_app = new HttpSendView();
			HttpSendViewModel client_context = new HttpSendViewModel();
			client_app.DataContext = client_context;
			client_app.Show();

			HttpReceiveView server_app = new HttpReceiveView();
			HttpReceiveViewModel server_context = new HttpReceiveViewModel();
			server_app.DataContext = server_context;
			server_app.Show();
		}
	}
}

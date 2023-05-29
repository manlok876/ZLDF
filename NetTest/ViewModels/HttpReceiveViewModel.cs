using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Net.Sockets;
using System.Net.WebSockets;

using Prism.Mvvm;
using Prism.Commands;

using System.Diagnostics;
using System.ComponentModel.Design;
using System.Windows.Input;
using System.Windows;
using System.IO;

namespace NetTest.ViewModels
{
	internal class HttpReceiveViewModel : BindableBase
	{
		public HttpListener Server
		{
			get;
			private set;
		} = new HttpListener();

		private string _requestBody;
		public string RequestBody
		{
			get
			{
				return _requestBody;
			}
			set
			{
				SetProperty(ref _requestBody, value);
			}
		}

		private string _responseBody;
		public string ResponseBody
		{
			get
			{
				return _responseBody;
			}
			set
			{
				SetProperty(ref _responseBody, value);
			}
		}

		public HttpListenerContext? CurrentContext
		{
			get;
			set;
		}

		public async void SendResponseAsync()
		{
			if (CurrentContext == null)
			{
				return;
			}

			HttpListenerResponse response = CurrentContext.Response;
			string responseText = ResponseBody;

			byte[] buffer = Encoding.UTF8.GetBytes(responseText);
			response.ContentLength64 = buffer.Length;
			using Stream output = response.OutputStream;

			await output.WriteAsync(buffer);
			await output.FlushAsync();

			CurrentContext = null;
		}

		public ICommand SendCommand { get; private set; }
		public void Send()
		{
			SendResponseAsync();
		}

		public async Task LoopListen()
		{
			while (true)
			{
				await ListenToRequest();
			}
		}

		public async Task ListenToRequest()
		{
			// получаем контекст
			HttpListenerContext context = await Server.GetContextAsync();

			if (CurrentContext != null)
			{
				CurrentContext.Response.Abort();
			}

			CurrentContext = context;

			StreamReader str = new StreamReader(context.Request.InputStream);
			string sBuf = str.ReadToEnd();
			RequestBody = sBuf;
		}

		public HttpReceiveViewModel()
		{
			SendCommand = new DelegateCommand(Send);

			Server.Prefixes.Add("http://localhost:8671/NetTest/");
			Server.Start();
			LoopListen();
		}

		public void HandleClosing()
		{
			Server.Stop();
		}
	}
}

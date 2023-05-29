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
using System.Windows.Input;
using System.Net.Mail;

namespace NetTest.ViewModels
{
	internal class HttpSendViewModel : BindableBase
	{
		public HttpClient Client
		{
			get;
			private set;
		} = new HttpClient();

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

		public Uri RequestURI
		{
			get
			{
				UriBuilder uriBuilder = new UriBuilder();

				uriBuilder.Scheme = "http";
				uriBuilder.Host = "localhost";
				uriBuilder.Port = 8671;
				uriBuilder.Path = "/NetTest";

				return uriBuilder.Uri;
			}
		}

		public async void SendRequestAsync()
		{
			StringContent content = new StringContent(RequestBody);
			using HttpRequestMessage request =
				new HttpRequestMessage(HttpMethod.Get, RequestURI);
			request.Content = content;

			using HttpResponseMessage response = await Client.SendAsync(request);

			Trace.WriteLine($"Status: {response.StatusCode}\n");

			Trace.WriteLine("Headers");
			foreach (var header in response.Headers)
			{
				Trace.Write($"{header.Key}:");
				foreach (var headerValue in header.Value)
				{
					Trace.WriteLine(headerValue);
				}
			}

			Trace.WriteLine("\nContent");
			string responseString = await response.Content.ReadAsStringAsync();
			Trace.WriteLine(responseString);
			ResponseBody = responseString;
		}

		public ICommand SendCommand { get; private set; }
		public void Send()
		{
			SendRequestAsync();
		}

		public HttpSendViewModel()
		{
			SendCommand = new DelegateCommand(Send);
		}
	}
}

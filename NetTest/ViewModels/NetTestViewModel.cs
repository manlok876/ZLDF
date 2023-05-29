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

namespace NetTest.ViewModels
{
	internal class NetTestViewModel : BindableBase
	{
		private IEnumerable<IPAddress?> GetIpsForDns()
		{
			string strHostName = Dns.GetHostName();
			IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

			return ipEntry.AddressList;
		}

		private IEnumerable<IPAddress?> GetIpsForNetworkAdapters()
		{

			var nics = from i in NetworkInterface.GetAllNetworkInterfaces()
					where i.OperationalStatus == OperationalStatus.Up && i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
					select new { name = i.Name, ip = GetIpFromUnicastAddresses(i) };

			return nics.Select(x => x.ip);
		}
		private IPAddress? GetWiFiIp()
		{
			var ips =
				from i in NetworkInterface.GetAllNetworkInterfaces()
				where i.OperationalStatus == OperationalStatus.Up &&
				i.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
				select GetIpFromUnicastAddresses(i);

			return ips.FirstOrDefault();
		}

		private IPAddress? GetIpFromUnicastAddresses(NetworkInterface i)
		{
			return (from ip in i.GetIPProperties().UnicastAddresses
					where ip.Address.AddressFamily == AddressFamily.InterNetwork
					select ip.Address).SingleOrDefault();
		}

		public IPAddress? WiFiIp
		{
			get
			{
				return GetWiFiIp();
			}
		}
		public IPEndPoint? WiFiEndpoint
		{
			get
			{
				IPAddress? ipWiFi = GetWiFiIp();
				if (ipWiFi == null)
				{
					return null;
				}
				return new IPEndPoint(ipWiFi, 8671);
			}
		}

		public string IpListString
		{
			get
			{
				string result = string.Empty;
				
				//IEnumerable<IPAddress?> addresses =
				//	GetIpsForNetworkAdapters();
				//foreach (IPAddress? addr in addresses)
				//{
				//	if (addr == null)
				//	{
				//		continue;
				//	}

				//	result += $"{addr.AddressFamily} {addr}\n";
				//}

				IPAddress? ipWiFi = GetWiFiIp();
				result += $"{ipWiFi?.AddressFamily} {ipWiFi}\n";

				return result;
			}
		}
	}
}

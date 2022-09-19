using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes.Logging
{
	public class StringMessage : Message
	{
		public string Payload { get; set; } = string.Empty;

		public override string ToString()
		{
			return $"[{Timestamp}]: {Payload}";
		}

		public StringMessage(string payload)
		{
			Payload = payload;
		}
	}
}

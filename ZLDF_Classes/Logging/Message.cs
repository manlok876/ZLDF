using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes.Logging
{
	public abstract class Message
	{
		public DateTime Timestamp { get; set; } = DateTime.Now;

		public override string ToString()
		{
			return $"[{Timestamp}]:";
		}

		public Message()
		{

		}
	}
}

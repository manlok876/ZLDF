using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLDF.Classes.Logging
{
	public abstract class Logger
	{
		public abstract void Log(Message message);

		public Logger()
		{

		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ZLDF.Classes.Logging
{
	public class FileLogger : Logger
	{
		private TextWriter _writer;
		private bool _disposed = false;

		private void CreateLogFile(string filePath)
		{
			_writer = new StreamWriter(filePath, true);
		}

		public void CloseLogFile()
		{
			Trace.WriteLine("Closed log file");
			if (_disposed)
			{
				return;
			}
			Flush();
			_writer.Close();
			_disposed = true;
		}

		public override void Log(Message message)
		{
			_writer.WriteLine(message.ToString());
		}

		public void Flush()
		{
			_writer.Flush();
		}

		public FileLogger(string filePath)
		{
			CreateLogFile(filePath);
		}

		~FileLogger()
		{
			CloseLogFile();
		}
	}
}

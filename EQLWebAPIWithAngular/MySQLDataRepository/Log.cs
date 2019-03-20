using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLDataRepository
{
	public class Log : ILog
	{
		private string _LogJSONString;
		public string LogData { get => _LogJSONString; set => _LogJSONString = value; }
	}
}

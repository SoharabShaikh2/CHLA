using DataRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySQLDataRepository
{
	public class MySqlConnectionParameters : IConnectionParameters
	{
		public string ConnectionString { get; set; }

	}
}

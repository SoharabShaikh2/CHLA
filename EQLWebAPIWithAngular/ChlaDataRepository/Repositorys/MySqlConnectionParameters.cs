using DataRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
	public class MySqlConnectionParameters : IConnectionParameters
	{
		public string ConnectionString { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository
{
   public interface IDataRepository<T>
    {
		

		 int Add(T data,string collectionName);

	}
}

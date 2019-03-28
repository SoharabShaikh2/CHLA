using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface ILogRepository<T>
    {
        void AddNewLog(string mainLog, string eveLog);
    }
}

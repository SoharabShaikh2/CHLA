using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface ILogRepository<T>
    {
        void AddNewLog(string mainLog, JObject eveLog);
        Task<JObject> GetDataFromDataBase(string sessionId);
    }
}

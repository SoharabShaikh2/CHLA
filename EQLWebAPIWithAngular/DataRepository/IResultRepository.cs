using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IResultRepository<T>
    {
        Task<List<ResultDto>> GetResults(string userId, string input, string dateTime);
        void AddNewResult(string userId, JObject result, string scenarioname, DateTime dateSession);
    }
}

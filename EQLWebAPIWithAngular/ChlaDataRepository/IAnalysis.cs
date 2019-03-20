using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public interface IAnalysis
    {
        JObject CalculateResult(JObject jsonData);
        void RegisterAnalyser(string name, IAnalyser analyser);
    }
}

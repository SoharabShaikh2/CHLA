using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    interface IAnalysis
    {
        JArray CalculateResult(JObject jsonData);
        void RegisterAnalyser(IAnalyser analyser);
    }
}

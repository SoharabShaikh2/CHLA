using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public interface IResultAnanlyser
    {
        JObject PerformAnalysis(JObject jsonData);
        void RegisterAnalysis(IAnalysis analysis, string name);
    }

}

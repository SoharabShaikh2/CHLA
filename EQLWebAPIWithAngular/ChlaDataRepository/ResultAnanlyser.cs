using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class ResultAnanlyser : IResultAnanlyser
    {

        Dictionary<string, IAnalysis> _analysises = new Dictionary<string, IAnalysis>();
        public ResultAnanlyser()
        {
        }

        public JObject PerformAnalysis(JObject jsonData)
        {
            JObject result = new JObject();
            result.Add("Details", GetDetails(jsonData));

            foreach (var kvp in _analysises)
            {
                result.Add(kvp.Key, kvp.Value.CalculateResult(jsonData));
            }

            return result;
        }


        public void RegisterAnalysis(IAnalysis analysis, string name)
        {
            _analysises.Add(name, analysis);
        }

        private JObject GetDetails(JObject jsonData)
        {
            throw new NotImplementedException();
        }


    }
}

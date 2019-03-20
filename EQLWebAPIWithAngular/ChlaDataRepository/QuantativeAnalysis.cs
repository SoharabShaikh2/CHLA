using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
   
    public class QuantativeAnalysis : IAnalysis
    {
        Dictionary<string, IAnalyser> _analyser = new Dictionary<string, IAnalyser>();

        public QuantativeAnalysis()
        {

        }
        public JObject CalculateResult(JObject jsonData)
        {
            JObject result = new JObject();
            foreach (var kvp in _analyser)
            {
                result.Add(kvp.Key, kvp.Value.Analyse(jsonData));
            }
            return result;

        }
        public void RegisterAnalyser(string name, IAnalyser analyser)
        {
            _analyser.Add(name, analyser);
        }
    }
}

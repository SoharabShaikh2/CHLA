using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public abstract class Analyser : IAnalyser
    {
        public JObject Analyse(JObject jsonObject)
        {
            return AnalyseAction(jsonObject);
        }
        //protected JObject SenarioStarted(JObject jsonobject);
        protected abstract JObject AnalyseAction(JObject jsonObject);
    }
}

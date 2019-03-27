using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    public abstract class Analysis : IAnalysis

    {

        List<IAnalyser> _analyser = new List<IAnalyser>();
        public Analysis()
        {
            Initialise();
        }




        public virtual JArray CalculateResult(JObject jsonData)
        {
            JArray result = new JArray();

            foreach (var analyser in _analyser)
            {
                var res = analyser.Analyse(jsonData);
                if(res.Count != 0)
                result.Add(res);
            }

            return result;
        }

        public virtual void RegisterAnalyser(IAnalyser analyser)
        {
            _analyser.Add(analyser);

        }

        protected abstract void Initialise();


    }
}

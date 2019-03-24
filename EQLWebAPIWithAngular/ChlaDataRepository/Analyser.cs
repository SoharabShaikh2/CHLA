using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    abstract class Analyser : IAnalyser
    {


        public string DisplayName { get; set; }

        public JObject Analyse(JObject jsonObject)
        {


            return AnalyseAction(jsonObject);


        }



        protected abstract JObject AnalyseAction(JObject jsonObject);

    }
}

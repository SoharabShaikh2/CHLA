using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChlaDataRepository
{
    class FirstPulsesCheckFromStartAnalyser : Analyser
    {
        public FirstPulsesCheckFromStartAnalyser()
        {
            DisplayName = "Time to first pulse check";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                List<string> check = new List<string>();
                string ScenarioStarted = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Anaphylaxis")
                    {
                        ScenarioStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        var time = currentrow.GetValue("Event_Time")?.ToString();
                        check.Add(time);
                    }

                }

                if (check.Count > 0 && ScenarioStarted != null)
                {
                    var timeInSecs =  long.Parse(check.OrderBy(x=>x).FirstOrDefault()) - long.Parse(ScenarioStarted);
                    var result = new JObject();
                    result.Add("DisplayTitle", DisplayName);
                    result.Add("DisplayValue", timeInSecs.ToString());
                    return result;

                }
            }

            return new JObject();
        }
    }
}

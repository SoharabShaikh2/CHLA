using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class FirstListeningToBreathSoundsAnalyser : Analyser
    {
        public FirstListeningToBreathSoundsAnalyser()
        {
            DisplayName = "Time to first breath check";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string check = null;
                string ScenarioStarted = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Anaphylaxis")
                    {
                        ScenarioStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BREATH" )
                    {
                        check = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    
                }

                if (check != null && ScenarioStarted != null)
                {
                    var timeInSecs = long.Parse(check) - long.Parse(ScenarioStarted);
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

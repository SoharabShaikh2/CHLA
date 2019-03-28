using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class FirstPulseCheckAnalyser : Analyser
    {
        public FirstPulseCheckAnalyser()
        {
            DisplayName = "First pulse check";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string ScenarioStarted = null;
                string CheckTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_STARTED" )
                    {
                        ScenarioStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        CheckTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (ScenarioStarted != null && CheckTime != null)
                {
                    var timeInSecs = long.Parse(CheckTime) - long.Parse(ScenarioStarted);
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

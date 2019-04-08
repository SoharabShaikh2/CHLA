using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChlaDataRepository
{
    class BloodGlucoseLevelAnalyser : Analyser
    {
        public BloodGlucoseLevelAnalyser()
        {
            DisplayName = "Time to first blood glucose level check";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string ScenarioStarted = null;
                List<string> CheckTime = new List<string>();
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_STARTED" )
                    {
                        ScenarioStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BLOOD_GLUCOSE")
                    {
                        CheckTime.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                }

                if (ScenarioStarted != null && CheckTime.Count > 0)
                {
                    var timeInSecs = long.Parse(CheckTime.OrderByDescending(x=>x).FirstOrDefault()) - long.Parse(ScenarioStarted);
                    if (timeInSecs > 0)
                    {
                        var result = new JObject();
                        result.Add("DisplayTitle", DisplayName);
                        result.Add("DisplayValue", timeInSecs.ToString());
                        return result;
                    }
                }
            }
            return new JObject();
        }
    }
}

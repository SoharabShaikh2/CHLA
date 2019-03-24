using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class BloodGlucoseLevelAnalyser : Analyser
    {
        public BloodGlucoseLevelAnalyser()
        {
            DisplayName = "Blood Glucose Level Checking";
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

                    if (currentrow.GetValue("Action")?.ToString() == "SCENARIO_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Adult_Seizure_Status_Epilepticus")
                    {
                        ScenarioStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("Action")?.ToString() == "CHECK_BLOOD_GLUCOSE")
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

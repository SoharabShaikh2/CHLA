using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class NRBmaskFromStartAnalyser : Analyser
    {
        public NRBmaskFromStartAnalyser()
        {
            DisplayName = "NRB Mask Used";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string ScenarioStarted = null;
                string maskUsedTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("Action")?.ToString() == "SCENARIO_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Adult_Seizure_Status_Epilepticus")
                    {
                        ScenarioStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("Action")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "NRBMaskTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        maskUsedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (ScenarioStarted != null && maskUsedTime != null)
                {
                    var timeInSecs = long.Parse(maskUsedTime) - long.Parse(ScenarioStarted);
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

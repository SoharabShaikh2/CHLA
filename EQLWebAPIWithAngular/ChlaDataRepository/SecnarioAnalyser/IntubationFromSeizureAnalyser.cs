using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class IntubationFromSeizureAnalyser : Analyser
    {
        public IntubationFromSeizureAnalyser()
        {
            DisplayName = "Intubation From Seizure";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string completeTime = null;
                string toolUsedTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "OBJECTIVE_COMPLETED" && currentrow.GetValue("ActionValue")?.ToString() == "SeizureCureObjective" && currentrow.GetValue("ActionOutcome")?.ToString() == "SUCCESSFUL")
                    {
                        completeTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolUsedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (completeTime != null && toolUsedTime != null)
                {
                    var timeInSecs = long.Parse(completeTime) - long.Parse(toolUsedTime);
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

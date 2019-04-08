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
            DisplayName = "Time to intubation after stopping seizure";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string completeTime = null;
                string toolUsedTime = null;
                bool diffCheck = false;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    if(currentrow.GetValue("Difficulty")?.ToString() == "ADVANCED")
                    {
                        diffCheck = true;
                    }
                    if (currentrow.GetValue("ActionID")?.ToString() == "OBJECTIVE_COMPLETED" && currentrow.GetValue("ActionValue")?.ToString() == "SeizureCureObjective" && currentrow.GetValue("ActionOutcome")?.ToString() == "SUCCESSFUL")
                    {
                        completeTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolUsedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (completeTime != null && toolUsedTime != null && diffCheck)
                {
                    var timeInSecs = long.Parse(toolUsedTime) - long.Parse(completeTime);
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

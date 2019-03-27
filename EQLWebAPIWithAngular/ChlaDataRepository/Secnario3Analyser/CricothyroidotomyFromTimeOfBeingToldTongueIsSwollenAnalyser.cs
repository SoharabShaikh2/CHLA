using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class CricothyroidotomyFromTimeOfBeingToldTongueIsSwollenAnalyser : Analyser
    {
        public CricothyroidotomyFromTimeOfBeingToldTongueIsSwollenAnalyser()
        {
            DisplayName = "Cricothyroidotomy From Time Of Being Told Tongue Is Swollen";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string startTime = null;
                string toolusedTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Tongue Swelling")
                    {
                        startTime = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "CricothyroidotomyTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolusedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (startTime != null && toolusedTime != null)
                {
                    var timeInSecs = long.Parse(toolusedTime) - long.Parse(startTime);
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

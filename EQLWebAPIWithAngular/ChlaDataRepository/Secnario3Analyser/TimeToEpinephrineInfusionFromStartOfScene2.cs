using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class TimeToEpinephrineInfusionFromStartOfScene2 : Analyser
    {
        public TimeToEpinephrineInfusionFromStartOfScene2()
        {
            DisplayName = "Time to Epinephrine infusion after medications given";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string startTime = null;
                string medUsedTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        startTime = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (startTime != null && medUsedTime != null)
                {
                    var timeInSecs = long.Parse(medUsedTime) - long.Parse(startTime);
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

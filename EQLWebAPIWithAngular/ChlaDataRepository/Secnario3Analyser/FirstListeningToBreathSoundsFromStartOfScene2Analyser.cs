using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class FirstListeningToBreathSoundsFromStartOfScene2Analyser : Analyser
    {
        public FirstListeningToBreathSoundsFromStartOfScene2Analyser()
        {
            DisplayName = "First Listening To Breath Sounds From Start Of Scene 2";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string startTime = null;
                string checkTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        startTime = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BREATH")
                    {
                        checkTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (startTime != null && checkTime != null)
                {
                    var timeInSecs = long.Parse(checkTime) - long.Parse(startTime);
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

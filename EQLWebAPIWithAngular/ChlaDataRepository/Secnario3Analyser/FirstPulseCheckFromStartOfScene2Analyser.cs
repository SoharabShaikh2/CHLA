using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChlaDataRepository
{
    class FirstPulseCheckFromStartOfScene2Analyser : Analyser
    {
        public FirstPulseCheckFromStartOfScene2Analyser()
        {
            DisplayName = "Time to first pulse check after medications given";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string startTime = null;
                List<string> checkTime = new List<string>();
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        startTime = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        checkTime.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                }

                if (startTime != null && checkTime.Count > 0)
                {
                    long timeInSecs = 0;
                    foreach (var check in checkTime.OrderBy(x => x))
                    {
                        timeInSecs = long.Parse(check) - long.Parse(startTime);
                        if (timeInSecs > 0)
                            break;
                    }
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

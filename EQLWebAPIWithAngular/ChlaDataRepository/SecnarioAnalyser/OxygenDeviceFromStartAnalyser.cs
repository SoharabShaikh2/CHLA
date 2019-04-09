using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class OxygenDeviceFromStartAnalyser : Analyser
    {
        public OxygenDeviceFromStartAnalyser()
        {
            DisplayName = "Time to first oxygen device";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string ScenarioStarted = null;
                string maskUsedTime1 = null;
                string maskUsedTime2 = null;
                string maskUsedTime3 = null;
                string maskUsedTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_STARTED" )//&& currentrow.GetValue("ActionValue")?.ToString() == "Seizure_Status_Epilepticus")
                    {
                        ScenarioStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "NRBMaskTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        maskUsedTime1 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "SimpleFaceMaskTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        maskUsedTime2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "NasalCannulaTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        maskUsedTime3 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                var t1 = 0l;
                var t2 = 0l;
                var t3 = 0l;


                if (maskUsedTime1 != null)
                {
                    t1 = long.Parse( maskUsedTime1);
                }
                 if (maskUsedTime2 != null)
                {
                    t2 = long.Parse(maskUsedTime2);
                }
                 if (maskUsedTime3 != null)
                {
                   t3 = long.Parse(maskUsedTime3);
                }

               
                if (maskUsedTime1 == null)
                {
                    t1 = long.MaxValue;
                }
                 if (maskUsedTime2 == null)
                {
                   t2 = long.MaxValue;
                }
                 if (maskUsedTime3 == null)
                {
                     t3 = long.MaxValue;
                }


                if (t1 < t2 && t1 < t3)
                {
                    maskUsedTime = t1.ToString();
                }
                else if (t2 < t1 && t2 < t3)
                {
                    maskUsedTime = t2.ToString();
                }
                else if (t3 < t1 && t3 < t2)
                {
                    maskUsedTime = t3.ToString();
                }
                else
                    maskUsedTime = null;




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

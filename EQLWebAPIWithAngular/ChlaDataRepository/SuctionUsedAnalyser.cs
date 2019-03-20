using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    public class SuctionUsedAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            long EventTime = 0;
            JToken ActionId = null;
            if (jsonObject.TryGetValue("ActionID", out ActionId))
            {
                JToken ActionValue = null;
                if (ActionId.ToString() == "TOOL_USED")
                {
                    if (jsonObject.TryGetValue("ActionValue", out ActionValue))
                    {
                        JToken ActionOutcome = null;
                        if (ActionValue.ToString() == "SuctionTool")
                        {
                            if (jsonObject.TryGetValue("ActionOutcome", out ActionOutcome))
                            {
                                if (ActionOutcome.ToString() == "ACTIVATED")
                                {
                                    JToken Event_Time = null;
                                    if (jsonObject.TryGetValue("Event_Time", out Event_Time))
                                    {
                                        if(Event_Time != null && !String.IsNullOrEmpty(Event_Time.ToString()))
                                        {
                                            EventTime = Convert.ToInt64(Event_Time);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            long PreEventTime = 0;

            if(EventTime > 0)
            {
                if (jsonObject.TryGetValue("ActionID", out ActionId))
                {
                    JToken ActionValue = null;
                    if (ActionId.ToString() == "SYMPTOM_CHANGED")
                    {
                        if (jsonObject.TryGetValue("ActionValue", out ActionValue))
                        {
                            JToken ActionOutcome = null;
                            if (ActionValue.ToString() == "Vomiting State")
                            {
                                if (jsonObject.TryGetValue("ActionOutcome", out ActionOutcome))
                                {
                                    if (ActionOutcome.ToString() == "ACTIVE")
                                    {
                                        JToken Event_Time = null;
                                        if (jsonObject.TryGetValue("Event_Time", out Event_Time))
                                        {
                                            if (Event_Time != null && !String.IsNullOrEmpty(Event_Time.ToString()))
                                            {
                                                PreEventTime = Convert.ToInt64(Event_Time);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            var resultObject = new JObject();
            resultObject.Add("Scenario_Time", (EventTime - PreEventTime));
            return resultObject;
        }
    }
}

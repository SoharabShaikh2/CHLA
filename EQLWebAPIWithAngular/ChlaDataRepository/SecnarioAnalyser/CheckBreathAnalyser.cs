using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class CheckBreathAnalyser :Analyser
    {
        public CheckBreathAnalyser()
        {
            DisplayName = "Check Breath";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string SymptomChanged = null;
                string CheckTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("Action")?.ToString() == "SYMPTOM_CHANGED" && currentrow.GetValue("ActionValue")?.ToString() == "Stop Breathing State" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVE")
                    {
                        SymptomChanged = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("Action")?.ToString() == "CHECK_BREATH")
                    {
                        CheckTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (SymptomChanged != null && CheckTime != null)
                {
                    var timeInSecs = long.Parse(CheckTime) - long.Parse(SymptomChanged);
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

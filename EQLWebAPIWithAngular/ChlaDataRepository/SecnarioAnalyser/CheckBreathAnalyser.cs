using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChlaDataRepository
{
    class CheckBreathAnalyser : Analyser
    {
        public CheckBreathAnalyser()
        {
            DisplayName = "Time to breath check after patient stopped breathing";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string SymptomChanged = null;
                List<string> CheckTime = new List<string>();
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (SymptomChanged == null && currentrow.GetValue("ActionID")?.ToString() == "SYMPTOM_CHANGED" && currentrow.GetValue("ActionValue")?.ToString() == "Stop Breathing State" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVE")
                    {
                        SymptomChanged = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BREATH")
                    {
                        CheckTime.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                }

                if (SymptomChanged != null && CheckTime.Count > 0)
                {
                    var dummArr = new List<string>{ "0" };
                    var arr = CheckTime.FindAll(ti => long.Parse(ti) > long.Parse(SymptomChanged));
                    if(arr==null || arr.Count==0)
                    {
                        arr = dummArr;
                    }
                    var timeInSecs = long.Parse((arr.OrderBy(x => x).FirstOrDefault())) - long.Parse(SymptomChanged);
                    if (timeInSecs > 0)
                    {
                        var result = new JObject();
                        result.Add("DisplayTitle", DisplayName);
                        result.Add("DisplayValue", timeInSecs.ToString());
                        return result;
                    }
                }
            }
            return new JObject();
        }
    }
}

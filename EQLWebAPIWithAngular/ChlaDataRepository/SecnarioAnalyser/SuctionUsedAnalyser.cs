using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class SuctionUsedAnalyser : Analyser
    {
        public SuctionUsedAnalyser()
        {
            DisplayName = "Suction Used";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string symtomChangedTime = null;
                string toolusedTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("Action")?.ToString() == "SYMPTOM_CHANGED" && currentrow.GetValue("ActionValue")?.ToString() == "Vomiting Stat" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVE")
                    {
                        symtomChangedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("Action")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "SuctionTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolusedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (symtomChangedTime != null && toolusedTime != null)
                {
                    var timeInSecs = long.Parse(toolusedTime) - long.Parse(symtomChangedTime);
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

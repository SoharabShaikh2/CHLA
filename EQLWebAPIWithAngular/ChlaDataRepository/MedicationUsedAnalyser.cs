using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class MedicationUsedAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string startTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_STARTED")
                    {
                        startTime = currentrow.GetValue("Event_Time")?.ToString();

                    }
                }
            }

            return new JObject();
        }
    }
}

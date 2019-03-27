using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class EpinephrineInjectionFromStartAnalyser : Analyser
    {
        public EpinephrineInjectionFromStartAnalyser()
        {
            DisplayName = "Epinephrine Injection From Start";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string med = null;
                //string med2 = null;
                string ScenarioStarted = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Anaphylaxis")
                    {
                        ScenarioStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineSyringeMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        med = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        med = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (med != null && ScenarioStarted != null)
                {
                    var timeInSecs = long.Parse(med) - long.Parse(ScenarioStarted);
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

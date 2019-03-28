using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class AdvancedFosphenytoinMedicationUsedAnalyser : Analyser
    {
        public AdvancedFosphenytoinMedicationUsedAnalyser()
        {
            DisplayName = "Time to Fosphenytoin after last dose of Lorazepam";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string MedicationUsedTime = null;
                string FosphenytoinUsedTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "AtivanIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        MedicationUsedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "FosphenytoinIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        FosphenytoinUsedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (MedicationUsedTime != null && FosphenytoinUsedTime != null)
                {
                    var timeInSecs = long.Parse(FosphenytoinUsedTime) - long.Parse(MedicationUsedTime);
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

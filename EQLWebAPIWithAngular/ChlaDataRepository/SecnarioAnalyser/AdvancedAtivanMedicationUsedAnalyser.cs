using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class AdvancedAtivanMedicationUsedAnalyser : Analyser
    {
        public AdvancedAtivanMedicationUsedAnalyser()
        {
            DisplayName = "Advanced Ativan Medication Used";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string MedicationUsedTime1 = null;
                string MedicationUsedTime2 = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "AtivanIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        if (MedicationUsedTime1 == null)
                        {
                            MedicationUsedTime1 = currentrow.GetValue("Event_Time")?.ToString();
                        }
                        else
                        {
                            MedicationUsedTime2 = currentrow.GetValue("Event_Time")?.ToString();
                        }
                    }
                }

                if (MedicationUsedTime1 != null && MedicationUsedTime2 != null)
                {
                    var timeInSecs = long.Parse(MedicationUsedTime2) - long.Parse(MedicationUsedTime1);
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

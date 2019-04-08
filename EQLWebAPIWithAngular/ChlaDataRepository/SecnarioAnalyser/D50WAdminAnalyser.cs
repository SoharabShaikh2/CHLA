using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChlaDataRepository
{
    class D50WAdminAnalyser : Analyser
    {
        public D50WAdminAnalyser()
        {
            DisplayName = "Time to glucose administration after first blood glucose check";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string MedicationUsedTime = null;
                string MedicationUsedTime1 = null;
                string MedicationUsedTime2 = null;
                List<string> CheckTime = new List<string>();
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BLOOD_GLUCOSE")
                    {
                        CheckTime.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "D50WIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        MedicationUsedTime1 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "GlucoseThiamineIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        MedicationUsedTime2 = currentrow.GetValue("Event_Time")?.ToString();
                    }

                }

                if (MedicationUsedTime1 != null)
                {
                    MedicationUsedTime = MedicationUsedTime1;
                }
                else if (MedicationUsedTime2 != null)
                {
                    MedicationUsedTime = MedicationUsedTime2;
                }

                if (MedicationUsedTime != null && CheckTime.Count > 0)
                {
                    var timeInSecs = long.Parse(MedicationUsedTime) - long.Parse(CheckTime.OrderBy(x => x).FirstOrDefault());
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

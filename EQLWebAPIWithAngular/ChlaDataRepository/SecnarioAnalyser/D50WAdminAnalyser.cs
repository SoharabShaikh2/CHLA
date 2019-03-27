using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class D50WAdminAnalyser : Analyser
    {
        public D50WAdminAnalyser()
        {
            DisplayName = "D50W Adminstration";
        }

        protected override JObject AnalyseAction(JObject jsonObject)
        {

            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string MedicationUsedTime = null;
                string CheckTime = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BREATH")
                    {
                        CheckTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "D50WIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        MedicationUsedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "GlucoseThiamineIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        MedicationUsedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }

                }

                if (MedicationUsedTime != null && CheckTime != null)
                {
                    var timeInSecs = long.Parse(CheckTime) - long.Parse(MedicationUsedTime);
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

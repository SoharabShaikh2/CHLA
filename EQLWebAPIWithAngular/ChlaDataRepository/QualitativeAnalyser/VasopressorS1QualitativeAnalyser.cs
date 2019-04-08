using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class VasopressorS1C_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string failTime = null;
                string medTime = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 5 - Hypotension" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        failTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionValue")?.ToString() == "DopamineIVMedication")
                    {
                        medTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if(failTime != null && medTime == null)
                {
                    ErrorType = "Critical";
                    Description = "Advanced: not choosing either dopamine or epinephrine after nurse says that ENT wants patient stable for OR";
                }
                
                if (ErrorType != null && DifficultyType == "ADVANCED")
                {
                    var result = new JObject();
                    result.Add("Category", "Vasopressor selection");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;
                }
            }
            return new JObject();
        }
    }

    class VasopressorS1M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string failTime = null;
                string medTime = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 5 - Hypotension" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        failTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionValue")?.ToString() == "DopamineIVMedication")
                    {
                        medTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                 if (failTime != null && medTime != null)
                {
                    ErrorType = "Moderate";
                    Description = "Advanced: Choosing Dopamine and not epinephrine drip when nurse says ENT wants patient stable for the OR";
                }

                if (ErrorType != null && DifficultyType == "ADVANCED")
                {
                    var result = new JObject();
                    result.Add("Category", "Vasopressor selection");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;
                }
            }
            return new JObject();
        }
    }
}

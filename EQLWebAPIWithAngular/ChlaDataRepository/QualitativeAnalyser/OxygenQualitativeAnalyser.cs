using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class Oxygen1QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        ErrorType = "Critical";
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "SimpleFaceMaskTool" || currentrow.GetValue("ActionValue")?.ToString() == "NasalCannulaTool") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        ErrorType = "Moderate";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Oxygen");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;

                }
            }
            return new JObject();
        }
    }


    class Oxygen2QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string secnce2Started = null;
                string secnce3Started = null;
                string intubationTool = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen")
                    {
                        secnce2Started = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 3 - Medications")
                    {
                        secnce3Started = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_FAILED" && currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool")
                    {
                        intubationTool = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_HE_IS_CYANOTIC")
                    {
                        ErrorType = "Moderate";
                    }
                }
                if (secnce2Started != null && secnce3Started != null && intubationTool != null)
                {
                    if (long.Parse(secnce2Started) < long.Parse(intubationTool) && long.Parse(intubationTool) > long.Parse(secnce3Started))
                    {
                        ErrorType = "Critical";
                    }
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Oxygen");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;

                }
            }
            return new JObject();
        }
    }
}

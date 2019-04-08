using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class WorseningS1QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string dialo = null;

                string tools = null;
                bool toolsBefo = false;
                bool toolsAfter = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_MEDICATION_NOT_INDICATED")
                    {
                        dialo = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool" || (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "NRBMaskTool" || currentrow.GetValue("ActionValue")?.ToString() == "SimpleFaceMaskTool" || currentrow.GetValue("ActionValue")?.ToString() == "NasalCannulaTool") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED"))
                    {
                        tools = currentrow.GetValue("Event_Time")?.ToString();
                        if (tools != null && dialo != null)
                        {
                            if (long.Parse(tools) > long.Parse(dialo))
                            {
                                toolsBefo = true;

                            }
                            else if (long.Parse(tools) < long.Parse(dialo))
                            {
                                toolsAfter = true;
                                
                            }

                        }
                    }

                }
                if (!toolsBefo && dialo != null)
                {
                    ErrorType = "Critical";
                    Description = "Not choosing Intubation or any other oxygen (100%NRB, facemask, nasal cannula) delivery After nurse warning “Doctor that won’t really help much in this situation”";
                }
                else if (!toolsAfter && dialo != null)
                {
                    ErrorType = "Moderate";
                    Description = "Choosing other oxygen delivery (100%NRB, facemask, nasal cannula) Prior to nurse warning “Doctor, that won’t really help much in this situation”";
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Worsening respiratory distress scene (Scene 2)");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class WorseningS2QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Tongue Swelling" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        ErrorType = "Critical";
                        Description = "Advanced: Not choosing Cricothyroidotomy after being told that the tongue is swollen";
                    }                 
                }
                
                if (ErrorType != null && DifficultyType == "ADVANCED")
                {
                    var result = new JObject();
                    result.Add("Category", "Worsening respiratory distress scene (Scene 2)");
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

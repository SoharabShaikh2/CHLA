using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class Oxygen1C_QualitativeAnalyser : Analyser
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

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        ErrorType = "Critical";
                        Description = "Not placing any oxygen delivery device (NRB, mask, nasal cannula)";
                        break;
                    }
                   
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Oxygen");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class Oxygen1M_QualitativeAnalyser : Analyser
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

                     if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "SimpleFaceMaskTool" || currentrow.GetValue("ActionValue")?.ToString() == "NasalCannulaTool") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        ErrorType = "Moderate";
                        Description = "Choosing other oxygen delivery device aside from NRB (face mask, nasal cannula)";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Oxygen");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }


    class Oxygen2C_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string secnce2Started = null;
                string secnce3Started = null;
                List<string> intubationTool = new List<string>();

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (secnce2Started == null && currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen")
                    {
                        secnce2Started = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (secnce3Started == null && currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 3 - Medications")
                    {
                        secnce3Started = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_FAILED" && currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool")
                    {
                        intubationTool .Add( currentrow.GetValue("Event_Time")?.ToString());
                    }
                    
                }
                if (secnce2Started != null && secnce3Started != null && intubationTool .Count>0)
                {

                    var intubtool = intubationTool.FindAll(itb => long.Parse(itb) >= long.Parse(secnce2Started) && long.Parse(itb) <= long.Parse(secnce3Started) );
                    if ( intubationTool !=null && intubationTool.Count>0)
                    {
                        ErrorType = "Critical";
                        Description = "Selecting intubation at this stage";
                    }
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Oxygen");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class Oxygen2M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
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

                if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_HE_IS_CYANOTIC")
                    {
                        ErrorType = "Moderate";
                        Description = "Waiting for Placement of oxygen device after being told by nurse “He is cyanotic”";
                        break;
                    }
                }
                
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Oxygen");
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

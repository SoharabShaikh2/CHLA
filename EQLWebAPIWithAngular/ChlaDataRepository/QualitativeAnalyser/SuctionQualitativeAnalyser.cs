using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class Suction1C_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string toolusedTime = null;
                string mainToolusedTime = null;
                bool modarateUsed = false;
                bool criticalUsed = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "SuctionTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        mainToolusedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "NRBMaskTool" || currentrow.GetValue("ActionValue")?.ToString() == "SimpleFaceMaskTool" || currentrow.GetValue("ActionValue")?.ToString() == "NasalCannulaTool") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolusedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_HE_IS_VOMITTING")
                    {
                        modarateUsed = true;
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_YOU_NEED_TO_SUCTION_NOW")
                    {
                        criticalUsed = true;
                    }

                }

                if (criticalUsed)
                {
                    ErrorType = "Critical";
                    Description = "Failure to suction completely prior to 2nd nurse reminder regarding suction (“Dr. You need to suction now”)";
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Suction");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }
    class Suction1M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string toolusedTime = null;
                string mainToolusedTime = null;
                bool modarateUsed = false;
                bool criticalUsed = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "SuctionTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        mainToolusedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "NRBMaskTool" || currentrow.GetValue("ActionValue")?.ToString() == "SimpleFaceMaskTool" || currentrow.GetValue("ActionValue")?.ToString() == "NasalCannulaTool") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolusedTime = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_HE_IS_VOMITTING")
                    {
                        modarateUsed = true;
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_YOU_NEED_TO_SUCTION_NOW")
                    {
                        criticalUsed = true;
                    }

                }

                if (modarateUsed)
                {
                    ErrorType = "Moderate";
                    Description = "Failure to suction prior to 1st nurse prompt of “He is vomiting”";
                }


                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Suction");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }
    class Suction1Mild_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string nrbtoolusedTime = null;
                List<string> toolusedTime = new List<string>();
                string suctionused = null;
                string simpleFaceMaskToolusedTime = null;
                string nasalCanulaToolusedTime = null;
                
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (suctionused == null && currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "SuctionTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        suctionused =  currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "NRBMaskTool"  && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolusedTime.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "SimpleFaceMaskTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolusedTime.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_USED" && currentrow.GetValue("ActionValue")?.ToString() == "NasalCannulaTool" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        toolusedTime.Add( currentrow.GetValue("Event_Time")?.ToString());
                    }

                 

                }

                if (toolusedTime != null && toolusedTime.Count>0 && suctionused !=null )
                {

                    var sucu = toolusedTime.FindAll(t => long.Parse(t) < long.Parse(suctionused));
                    if (sucu !=null && sucu.Count>0)
                    {
                        ErrorType = "Mild";
                        Description = "Placement of oxygen after vomiting prior to suction ";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Suction");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class Suction2C_QualitativeAnalyser : Analyser
    {

        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string secnceStarted = null;
                string toolFeaild = null;
                string madicationFaild = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen")
                    {
                        secnceStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_FAILED" && currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool")
                    {
                        toolFeaild = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_FAILED")
                    {
                        madicationFaild = currentrow.GetValue("Event_Time")?.ToString();
                    }

                }

                if (secnceStarted != null && toolFeaild != null)
                {
                    if (long.Parse(toolFeaild) < long.Parse(secnceStarted))
                    {
                        ErrorType = "Critical";
                        Description = "Choosing to intubate at this stage (Nurse block- “early for intubation”)";
                    }
                }


                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Suction");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class Suction2M_QualitativeAnalyser : Analyser
    {

        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string secnceStarted = null;
                string toolFeaild = null;
                string madicationFaild = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen")
                    {
                        secnceStarted = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_FAILED" && currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool")
                    {
                        toolFeaild = currentrow.GetValue("Event_Time")?.ToString();
                    }

                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_FAILED")
                    {
                        madicationFaild = currentrow.GetValue("Event_Time")?.ToString();
                    }

                }

                if (secnceStarted != null && madicationFaild != null)
                {
                    if (long.Parse(madicationFaild) < long.Parse(secnceStarted))
                    {
                        ErrorType = "Moderate";
                        Description = "Choosing medication prior to suction and oxygen";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Suction");
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

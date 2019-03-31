using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class Medications1QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string dialoguePlayed = null;
                string IVtoolFailed = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_MEDICATION_REMINDER2")
                    {
                        ErrorType = "Crtical";
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_MEDICATION_REMINDER")
                    {
                        ErrorType = "Moderate";
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_NO_IV_ACCESS")
                    {
                        dialoguePlayed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "IV_TOOL_FAILED" && currentrow.GetValue("ActionValue")?.ToString() == "AtivanIVMedication")
                    {
                        IVtoolFailed = currentrow.GetValue("Event_Time")?.ToString();
                    }

                }

                if (dialoguePlayed != null && IVtoolFailed != null)
                {
                    if (long.Parse(dialoguePlayed) > long.Parse(IVtoolFailed))
                    {
                        ErrorType = "Mild";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;

                }
            }
            return new JObject();
        }
    }

    class Medications2QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string medicationUsed = null;
                string medicationBefore = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionValue")?.ToString() == "AdenosineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "AtropineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "DopamineIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PropranololTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "RacemicEpinephrineNebulizerMedication" || currentrow.GetValue("ActionValue")?.ToString() == "AlbuterolNebulizerMedication")
                    {
                        ErrorType = "Crtical";
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "AtivanIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medicationUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionValue")?.ToString() == "FosphenytoinIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "KeppraIVMedication")
                    {
                        medicationBefore = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionValue")?.ToString() == "CeftriaxoneIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "BenadrylIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "BenadrylTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "SoluMedrolIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PrednisoneTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "RanitidineIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "RanitidineTabletMedication")
                    {
                        ErrorType = "Mild";
                    }
                }
                if (medicationUsed != null && medicationBefore != null)
                {
                    if (long.Parse(medicationBefore) < long.Parse(medicationUsed))
                    {
                        ErrorType = "Moderate";
                    }
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;

                }
            }
            return new JObject();
        }
    }

    class Medications3QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string scenceEnd = null;
                string medicationUsed = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Advance Status / Intubation" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        scenceEnd = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "AtivanIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medicationUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("Difficulty")?.ToString() == "ADVANCED" && (currentrow.GetValue("ActionValue")?.ToString() == "XanaxTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "ValiumTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "BenadrylTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PrednisoneTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PropranololTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "RanitidineTabletMedication"))
                    {
                        ErrorType = "Moderate";
                    }
                }
                if (medicationUsed == null || scenceEnd == null)
                {
                    ErrorType = "Critical";
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;

                }
            }
            return new JObject();
        }
    }

    class Medications4QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string scenceEnd = null;
                string medicationUsed = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Advance Status / Intubation" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        scenceEnd = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "AtivanIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medicationUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }         
                }
                if (medicationUsed != null || scenceEnd != null)
                {
                    ErrorType = "Critical";
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;
                }
            }
            return new JObject();
        }
    }

    class Medications5QualitativeAnalyser : Analyser
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

                    if (currentrow.GetValue("ActionID")?.ToString() == "TOOL_FAILED" && currentrow.GetValue("ActionValue")?.ToString() == "CricothyroidotomyTool")
                    {
                        ErrorType = "Critical";
                    }              
                }
                
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;
                }
            }
            return new JObject();
        }
    }

    class Medications6QualitativeAnalyser : Analyser
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

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 5 - Advanced Airway" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        ErrorType = "Critical";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;
                }
            }
            return new JObject();
        }
    }

    class Medications7QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sStart = null;
                string mUsed = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 5 - Advanced Airway")
                    {
                        sStart = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" || currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_FAILED")
                    {
                        mUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (sStart != null && mUsed != null)
                {
                    if (long.Parse(sStart) > long.Parse(mUsed))
                    {
                        ErrorType = "Critical";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
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

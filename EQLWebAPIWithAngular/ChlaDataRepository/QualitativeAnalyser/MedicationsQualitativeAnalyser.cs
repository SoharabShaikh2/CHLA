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
            string Description = null;
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
                        Description = "Failure to choose lorazepam prior to second warning by nurse";
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_MEDICATION_REMINDER")
                    {
                        ErrorType = "Moderate";
                        Description = "Failure to choose lorazepam prior to first warning from nurse";
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
                        Description = "Lorazepam prior to IV placement";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
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
            string Description = null;
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
                        Description = "Choosing a medication that is wrong and may cause harm (adenosine, atropine, epinephrine, dopamine, propranolol, racemic epinephrine, albuterol)";
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
                        Description = "Choosing a medication that is wrong but no harm to patient (ceftriaxone, diphenhydramine, methylprednisolone, ranitidine)";
                    }
                }
                if (medicationUsed != null && medicationBefore != null)
                {
                    if (long.Parse(medicationBefore) < long.Parse(medicationUsed))
                    {
                        ErrorType = "Moderate";
                        Description = "Choosing Fosphenytoin or levetiracetam prior to choosing lorazepam";
                    }
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
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
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string scenceEnd = null;
                List<string> medicationUsed = new List<string>();

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
                        medicationUsed.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("Difficulty")?.ToString() == "ADVANCED" && (currentrow.GetValue("ActionValue")?.ToString() == "XanaxTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "ValiumTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "BenadrylTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PrednisoneTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PropranololTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "RanitidineTabletMedication"))
                    {
                        ErrorType = "Moderate";
                        Description = "Choosing any tablet medication at this stage";
                    }
                }
                if (medicationUsed.Count != 2 && scenceEnd != null)
                {
                    ErrorType = "Critical";
                    Description = "Advanced: failure to give second dose of lorazepam after nurse warning “Doctor the kid is still seizing”";
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
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
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string scenceEnd = null;
                List<string> medicationUsed = new List<string>();

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
                        medicationUsed.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                }
                if (medicationUsed.Count == 2 && scenceEnd != null)
                {
                    ErrorType = "Critical";
                    Description = "Advanced: Failure to give Fosphenytoin";
                }
                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
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
            string Description = null;
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
                        Description = "Advanced: Choosing Cricothyroidotomy in this scenario";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
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
            string Description = null;
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
                        Description = "Advanced: Failure to intubate at all";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
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
            string Description = null;
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
                    if (long.Parse(sStart) < long.Parse(mUsed))
                    {
                        ErrorType = "Critical";
                        Description = "Advanced: Choosing more medications after seizure stops prior to intubation";
                    }
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;
                }
            }
            return new JObject();
        }
    }

    class Medications8_1QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sStart = null;
                string mUsed = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "AtivanIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        sStart = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BLOOD_GLUCOSE")
                    {
                        mUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (sStart != null && mUsed != null)
                {
                    if (long.Parse(sStart) < long.Parse(mUsed))
                    {
                        ErrorType = "Critical";
                        Description = "Not checking blood glucose level before the first Ativan";
                    }
                }
                else if (sStart != null && mUsed == null)
                {
                    ErrorType = "Critical";
                    Description = "Not checking blood glucose level before the first Ativan";
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;
                }
            }
            return new JObject();
        }
    }

    class Medications8_2QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sStart = null;
                string mUsed = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "D50WIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        sStart = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BLOOD_GLUCOSE")
                    {
                        mUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (sStart != null && mUsed != null)
                {
                    if (long.Parse(sStart) > long.Parse(mUsed))
                    {
                        ErrorType = null;
                    }
                    else
                    {
                        ErrorType = "Critical";
                        Description = "Not giving D50W after checking first blood glucose level (low)";
                    }
                }
                else if (sStart == null && mUsed != null)
                {
                    ErrorType = "Critical";
                    Description = "Not giving D50W after checking first blood glucose level (low)";
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;
                }
            }
            return new JObject();
        }
    }

    class Medications8_3QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                List<string> mUsed = new List<string>();
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "D50WIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        mUsed.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                }

                if (mUsed.Count > 1)
                {
                    ErrorType = "Mild";
                    Description = "Giving multiple doses of D50W";
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;
                }
            }
            return new JObject();
        }
    }

    class Medications8_4QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sStart = null;
                string mUsed = null;
                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "D50WIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "GlucoseThiamineIVMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        sStart = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BLOOD_GLUCOSE")
                    {
                        mUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (sStart != null && mUsed != null)
                {
                    if (long.Parse(sStart) < long.Parse(mUsed))
                    {
                        ErrorType = null;
                    }
                    else
                    {
                        ErrorType = "Moderate";
                        Description = "Not checking blood glucose after Dextrose infusion (D50W)";
                    }
                }
                else if (sStart != null && mUsed == null)
                {
                    ErrorType = "Moderate";
                    Description = "Not checking blood glucose after Dextrose infusion (D50W)";
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Medications");
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

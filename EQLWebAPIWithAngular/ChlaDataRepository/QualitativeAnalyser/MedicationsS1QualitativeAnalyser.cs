using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class MedicationsS1C_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sceEnd = null;
                string medUsed = null;
                string medUsed2 = null;
                string medUsed3 = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 1 - Medication" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        sceEnd = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "AlbuterolNebulizerMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "SoluMedrolIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PrednisoneTabletMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "RanitidineTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "RanitidineIVMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed3 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }
                if (sceEnd != null && medUsed != null)
                {
                    ErrorType = "Critical";
                    Description = "Failure to select epinephrine injection or albuterol/racemic epinephrine";
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

    class MedicationsS1M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sceEnd = null;
                string medUsed = null;
                string medUsed2 = null;
                string medUsed3 = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 1 - Medication" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        sceEnd = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "AlbuterolNebulizerMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "SoluMedrolIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PrednisoneTabletMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "RanitidineTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "RanitidineIVMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed3 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }
                if (sceEnd != null && medUsed2 == null)
                {
                    ErrorType = "Moderate";
                    Description = "Failure to select methylprednisolone (Moderate or advanced) or prednisone /dexamethasone(beginner)";
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

    class MedicationsS1Mild_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sceEnd = null;
                string medUsed = null;
                string medUsed2 = null;
                string medUsed3 = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_ENDED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 1 - Medication" && currentrow.GetValue("ActionOutcome")?.ToString() == "FAILED")
                    {
                        sceEnd = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "AlbuterolNebulizerMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "SoluMedrolIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PrednisoneTabletMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "RanitidineTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "RanitidineIVMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed3 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }
                if (medUsed3 == null)
                {
                    ErrorType = "Mild";
                    Description = "Failure to select Ranitidine";
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

    class MedicationsS2M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sceStr = null;
                string medUsed = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 1 - Medication")
                    {
                        sceStr = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    

                }
                if (sceStr != null && medUsed != null)
                {
                    if (long.Parse(sceStr) > long.Parse(medUsed))
                    {
                        ErrorType = "Moderate";
                        Description = "Choosing epinephrine INFUSION at this stage.";
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

    class MedicationsS2Mild_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string sceStr = null;
                string medUsed = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 1 - Medication")
                    {
                        sceStr = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S1_NA_NO_IV_ACCESS")
                    {
                        ErrorType = "Mild";
                        Description = "Being blocked by nurse for not having IV access";
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

    class MedicationsS3QualitativeAnalyser : Analyser
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

                    if (currentrow.GetValue("ActionValue")?.ToString() == "AdenosineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "AtropineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "DopamineIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "PropranololTabletMedication" || currentrow.GetValue("ActionValue")?.ToString() == "AtivanIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "FosphenytoinIVMedication" || currentrow.GetValue("ActionValue")?.ToString() == "XanaxTabletMedication")
                    {
                        ErrorType = "Critical";
                        Description = "Choosing a medication that is wrong and may cause harm (adenosine, atropine, dopamine, propranolol, Lorazepam, Fosphenytoin, Alprazolam)";
                    }
                    else if (currentrow.GetValue("ActionValue")?.ToString() == "CeftriaxoneIVMedication")
                    {
                        ErrorType = "Mild";
                        Description = "Choosing a medication that is wrong but no harm to patient (ceftriaxone)";
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

    class MedicationsS4C_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string medUsed = null;
                string medUsed2 = null;
                string mainMedUsed = null;
                string secn2 = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "AlbuterolNebulizerMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool")
                    {
                        mainMedUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        secn2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }
                if (mainMedUsed != null && medUsed != null && medUsed2 != null)
                {
                    if (long.Parse(mainMedUsed) < long.Parse(medUsed) && long.Parse(mainMedUsed) < long.Parse(medUsed2))
                    {
                        ErrorType = "Critical";
                        Description = "Choosing intubation prior to epinephrine and albuterol";
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

    class MedicationsS4M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string medUsed = null;
                string medUsed2 = null;
                string mainMedUsed = null;
                string secn2 = null;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && (currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineSyringeMedication" || currentrow.GetValue("ActionValue")?.ToString() == "EpinephrineIVMedication") && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "MEDICATION_USED" && currentrow.GetValue("ActionValue")?.ToString() == "AlbuterolNebulizerMedication" && currentrow.GetValue("ActionOutcome")?.ToString() == "ACTIVATED")
                    {
                        medUsed2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionValue")?.ToString() == "IntubationTool")
                    {
                        mainMedUsed = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        secn2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }
                if (mainMedUsed != null && medUsed != null && medUsed2 != null && secn2 != null)
                {
                    if (long.Parse(mainMedUsed) > long.Parse(medUsed) && long.Parse(mainMedUsed) > long.Parse(medUsed2) && long.Parse(mainMedUsed) < long.Parse(secn2))
                    {
                        ErrorType = "Moderate";
                        Description = "Choosing intubation after albuterol and epinephrine but before other medications";
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


}

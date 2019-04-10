using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class PhysicalExam1C_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string scene1 = null;
                string scene2 = null;
                string scene3 = null;
                List<string> pupilsArry = new List<string>(); ;
                bool pulshCheck = false;
                bool pulshCheck2 = false;
                bool pulshCheck3 = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (scene2 == null && currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen")
                    {
                        scene2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (scene3 == null && currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 3 - Medications")
                    {
                        scene3 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                  
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PUPILS")
                    {
                        pupilsArry.Add(currentrow.GetValue("Event_Time")?.ToString());


                    }
                }





                if (scene2 != null && scene3 != null && pupilsArry != null && pupilsArry.Count > 0)
                {
                    var pupil = pupilsArry.FindAll(pp => long.Parse(pp) >= long.Parse(scene2) && long.Parse(pp) <= long.Parse(scene3));
                    if (pupil != null && pupil.Count == 0)
                    {
                        ErrorType = "Critical";
                        Description = "Failure to check pupils prior to Scene 3";
                    }
                }


                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class PhysicalExam1M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string scene1 = null;
                string scene2 = null;
                string scene3 = null;
                List<string> pupilsArry = new List<string>(); ;
                bool pulshCheck = false;
                bool pulshCheck2 = false;
                bool pulshCheck3 = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen")
                    {
                        scene2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 3 – Medications")
                    {
                        scene3 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 1 - Suction")
                    {
                        scene1 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PUPILS")
                    {
                        pupilsArry.Add(currentrow.GetValue("Event_Time")?.ToString());


                    }
                }

                foreach (var pupils in pupilsArry)
                {
                    if (scene2 != null && scene3 != null && pupils != null)
                    {
                        if (long.Parse(scene2) < long.Parse(pupils) && long.Parse(scene3) > long.Parse(pupils))
                        {
                            pulshCheck = true;

                        }
                    }
                    else if (scene2 != null && scene1 != null && pupils != null)
                    {
                        if (long.Parse(scene1) < long.Parse(pupils) && long.Parse(scene2) > long.Parse(pupils))
                        {
                            pulshCheck2 = true;
                        }
                    }
                    else if (scene1 != null && pupils != null)
                    {
                        if (long.Parse(scene1) > long.Parse(pupils))
                        {
                            pulshCheck3 = true;
                        }
                    }
                }

                if (scene2 != null && scene1 != null && !pulshCheck2)
                {
                    ErrorType = "Moderate";
                    Description = "Failure to check pupils prior to scene 2";

                }


                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class PhysicalExam1Mild_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string scene1 = null;
                string scene2 = null;
                string scene3 = null;
                List<string> pupilsArry = new List<string>(); ;
                bool pulshCheck = false;
                bool pulshCheck2 = false;
                bool pulshCheck3 = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Oxygen")
                    {
                        scene2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 3 – Medications")
                    {
                        scene3 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 1 - Suction")
                    {
                        scene1 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PUPILS")
                    {
                        pupilsArry.Add(currentrow.GetValue("Event_Time")?.ToString());


                    }
                }

                foreach (var pupils in pupilsArry)
                {
                    if (scene2 != null && scene3 != null && pupils != null)
                    {
                        if (long.Parse(scene2) < long.Parse(pupils) && long.Parse(scene3) > long.Parse(pupils))
                        {
                            pulshCheck = true;

                        }
                    }
                    else if (scene2 != null && scene1 != null && pupils != null)
                    {
                        if (long.Parse(scene1) < long.Parse(pupils) && long.Parse(scene2) > long.Parse(pupils))
                        {
                            pulshCheck2 = true;
                        }
                    }
                    else if (scene1 != null && pupils != null)
                    {
                        if (long.Parse(scene1) > long.Parse(pupils))
                        {
                            pulshCheck3 = true;
                        }
                    }
                }

                // if (scene1 != null && !pulshCheck3)
                //{
                //    ErrorType = "Mild";
                //    Description = "Failure to check pupils during scene1";
                //}

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class PhysicalExam2C_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string scene4 = null;
                List<string> pulshArry = new List<string>();
                bool pulshCheck = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Advance Status / Intubation")
                    {
                        scene4 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        pulshArry.Add(currentrow.GetValue("Event_Time")?.ToString());

                    }
             
                }

                foreach (var pulsh in pulshArry)
                {
                    if (scene4 != null && pulsh != null)
                    {
                        if (long.Parse(scene4) < long.Parse(pulsh))
                        {
                            pulshCheck = true;
                        }
                    }
                }

                if (scene4 != null && !pulshCheck)
                {
                    ErrorType = "Critical";
                    Description = "Advanced: Failure to check pulses during advanced scene";
                }

                if (ErrorType != null && DifficultyType == "ADVANCED")
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class PhysicalExam2M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string scene4 = null;
                List<string> pulshArry = new List<string>();
                bool pulshCheck = false;
                bool findAction = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Advance Status / Intubation")
                    {
                        scene4 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        pulshArry.Add(currentrow.GetValue("Event_Time")?.ToString());

                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_CAPILLARY_REFILL")
                    {
                        findAction = true;
                       
                    }
                }

                

                if (!findAction)
                {
                    ErrorType = "Moderate";
                    Description = "Failure to check capillary refill at all";

                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class PhysicalExam3C_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string scene4 = null;
                List<string> breathArry = new List<string>();
                List<string> pulshArry = new List<string>();
                bool breathCheck = false;
                bool pulshCheck = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Advance Status / Intubation")
                    {
                        scene4 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BREATH")
                    {
                        breathArry.Add(currentrow.GetValue("Event_Time")?.ToString());

                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        pulshArry.Add(currentrow.GetValue("Event_Time")?.ToString());

                    }
                }
                foreach (var breath in breathArry)
                {
                    if (scene4 != null && breath != null)
                    {
                        if (long.Parse(scene4) < long.Parse(breath))
                        {
                            breathCheck = true;

                        }
                    }
                }
                foreach (var pulsh in pulshArry)
                {
                    if (scene4 != null && pulsh != null)
                    {
                        if (long.Parse(scene4) > long.Parse(pulsh))
                        {
                            pulshCheck = true;
                        }
                    }
                }

                if (scene4 != null && !breathCheck)
                {
                    ErrorType = "Critical";
                    Description = "Advanced: Failure to listen to breath sounds during advance scene (breathing stops)";
                }
               

                if (ErrorType != null && DifficultyType == "ADVANCED")
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
                    result.Add("DifficultyType", DifficultyType == "BEGINNER" ? "Standard" : DifficultyType == "ADVANCED" ? "Advanced" : DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", Description);
                    return result;

                }
            }
            return new JObject();
        }
    }

    class PhysicalExam3M_QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string scene4 = null;
                List<string> breathArry = new List<string>();
                List<string> pulshArry = new List<string>();
                bool breathCheck = false;
                bool pulshCheck = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;

                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Advance Status / Intubation")
                    {
                        scene4 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BREATH")
                    {
                        breathArry.Add(currentrow.GetValue("Event_Time")?.ToString());

                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        pulshArry.Add(currentrow.GetValue("Event_Time")?.ToString());

                    }
                }
                foreach (var breath in breathArry)
                {
                    if (scene4 != null && breath != null)
                    {
                        if (long.Parse(scene4) < long.Parse(breath))
                        {
                            breathCheck = true;

                        }
                    }
                }
                foreach (var pulsh in pulshArry)
                {
                    if (scene4 != null && pulsh != null)
                    {
                        if (long.Parse(scene4) > long.Parse(pulsh))
                        {
                            pulshCheck = true;
                        }
                    }
                }

                 if (scene4 != null && !pulshCheck)
                {
                    ErrorType = "Moderate";
                    Description = "Advanced: Failure to check pulses prior to advanced scene";
                }

                if (ErrorType != null && DifficultyType == "ADVANCED")
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
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

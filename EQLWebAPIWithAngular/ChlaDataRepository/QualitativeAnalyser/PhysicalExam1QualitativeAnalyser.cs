using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ChlaDataRepository
{
    class PhysicalExam1QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                string scene1 = null;
                string scene2 = null;
                string scene3 = null;
                string pupils = null;
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
                        pupils = currentrow.GetValue("Event_Time")?.ToString();

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
                }

                if (!pulshCheck)
                {
                    ErrorType = "Critical";
                }
                else if (!pulshCheck2)
                {
                    ErrorType = "Moderate";

                }
                else if (!pulshCheck3)
                {
                    ErrorType = "Mild";
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;

                }
            }
            return new JObject();
        }
    }

    class PhysicalExam2QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string scene4 = null;
                string pulsh = null;
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
                        pulsh = currentrow.GetValue("Event_Time")?.ToString();
                        if (scene4 != null && pulsh != null)
                        {
                            if (long.Parse(scene4) < long.Parse(pulsh))
                            {
                                pulshCheck = true;
                            }
                        }
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_CAPILLARY_REFILL")
                    {
                        ErrorType = "Moderate";
                    }
                }

                if (!pulshCheck)
                {
                    ErrorType = "Critical";
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
                    result.Add("DifficultyType", DifficultyType);
                    result.Add("ErrorType", ErrorType);
                    result.Add("Description", "Example qualitative data");
                    return result;

                }
            }
            return new JObject();
        }
    }

    class PhysicalExam3QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                string scene4 = null;
                string breath = null;
                string pulsh = null;
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
                        breath = currentrow.GetValue("Event_Time")?.ToString();
                        if (scene4 != null && breath != null)
                        {
                            if (long.Parse(scene4) < long.Parse(breath))
                            {
                                breathCheck = true;

                            }
                        }
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        pulsh = currentrow.GetValue("Event_Time")?.ToString();
                        if (scene4 != null && pulsh != null)
                        {
                            if (long.Parse(scene4) > long.Parse(pulsh))
                            {
                                pulshCheck = true;
                            }
                        }
                    }
                }

                if (!breathCheck)
                {
                    ErrorType = "Critical";
                }
                else if (!pulshCheck)
                {
                    ErrorType = "Moderate";
                }

                if (ErrorType != null)
                {
                    var result = new JObject();
                    result.Add("Category", "Physical Exam");
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

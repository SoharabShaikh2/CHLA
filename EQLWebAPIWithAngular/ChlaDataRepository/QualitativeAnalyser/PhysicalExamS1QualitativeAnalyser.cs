using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChlaDataRepository
{
    class PhysicalExamS1QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                List<string> check = new List<string>();
                List<string> check2 = new List<string>();
                string dia = null;
                string sce = null;
                bool check1P = false;
                bool check2P = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_BREATH")
                    {
                        check.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "DIALOGUE_PLAYED" && currentrow.GetValue("ActionValue")?.ToString() == "NurseAngelCharacter" && currentrow.GetValue("ActionOutcome")?.ToString() == "S2_NA_CHILD_WHEEZING")
                    {
                        dia = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_CAPILLARY_REFILL")
                    {
                        check2.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        sce = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

                if (check.Count == 0)
                {
                    ErrorType = "Critical";
                    Description = "Not listening to breath sounds after being told child is wheezing";

                }
                else if (check.Count > 0 && dia != null)
                {
                    foreach (var p in check)
                    {
                        if (long.Parse(p) < long.Parse(dia))
                        {
                            check1P = true;
                        }

                    }
                }
                else if (check2.Count > 0 && sce != null)
                {
                    foreach (var p in check2)
                    {
                        if (long.Parse(p) > long.Parse(sce))
                        {
                            check2P = true;
                        }
                    }
                }

                if (!check1P && dia != null)
                {
                    ErrorType = "Moderate";
                    Description = "Not listening to breath sounds prior to being told the patient is wheezing";
                }
                else if (!check2P && sce != null)
                {
                    ErrorType = "Mild";
                    Description = "Not checking capillary refill during scene 1";
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

    class PhysicalExamS2QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                List<string> check = new List<string>();

                string sce = null;
                bool check1P = false;
                bool check2P = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();

                    if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        check.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        sce = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }


                if (check.Count > 0 && sce != null)
                {
                    foreach (var p in check)
                    {
                        if (long.Parse(p) < long.Parse(sce))
                        {
                            check1P = true;
                        }

                    }
                }
                else if (check.Count > 0 && sce != null)
                {
                    foreach (var p in check)
                    {
                        if (long.Parse(p) - long.Parse(sce) < 60)
                        {
                            check2P = true;
                        }
                    }
                }
                if (!check1P && sce != null)
                {
                    ErrorType = "Critical";
                    Description = "Not checking pulses at all during scene 1";
                }
                else if (!check2P && sce != null)
                {
                    ErrorType = "Moderate";
                    Description = "Not checking pulses within first 1 minute";
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


    class PhysicalExamS3QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {
                
                List<string> check2 = new List<string>();
                
                string sce = null;
                string sce2 = null;
                bool check1P = false;
                bool check2P = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();


                    if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_CAPILLARY_REFILL")
                    {
                        check2.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        sce = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Tongue Swelling")
                    {
                        sce2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }

               
                 if (check2.Count > 0 && sce != null && DifficultyType != "ADVANCED")
                {
                    foreach (var p in check2)
                    {
                        if (long.Parse(p) > long.Parse(sce))
                        {
                            check1P = true;
                        }
                    }
                }
                else if (check2.Count > 0 && sce2 != null && DifficultyType == "ADVANCED")
                {
                    foreach (var p in check2)
                    {
                        if (long.Parse(p) < long.Parse(sce2) && long.Parse(p) > long.Parse(sce))
                        {
                            check2P = true;
                        }
                    }
                }

                if (!check1P && sce != null)
                {
                    ErrorType = "Critical";
                    Description = "Not checking capillary refill during scene 2";
                }
                else if (!check2P && sce2 != null)
                {
                    ErrorType = "Critical";
                    Description = "Not checking capillary refill during scene 2";
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

    class PhysicalExamS4QualitativeAnalyser : Analyser
    {
        protected override JObject AnalyseAction(JObject jsonObject)
        {
            string ErrorType = null;
            string DifficultyType = null;
            string Description = null;
            var jarr = (JArray)jsonObject.GetValue("Events");
            if (jarr != null)
            {

                List<string> check2 = new List<string>();

                string sce = null;
                string sce2 = null;
                bool check1P = false;
                bool check2P = false;

                foreach (var jobj in jarr)
                {
                    var currentrow = (JObject)jobj;
                    DifficultyType = currentrow.GetValue("Difficulty")?.ToString();


                    if (currentrow.GetValue("ActionID")?.ToString() == "CHECK_PULSE")
                    {
                        check2.Add(currentrow.GetValue("Event_Time")?.ToString());
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 2 - Worsening respiratory distress")
                    {
                        sce = currentrow.GetValue("Event_Time")?.ToString();
                    }
                    else if (currentrow.GetValue("ActionID")?.ToString() == "SCENE_STARTED" && currentrow.GetValue("ActionValue")?.ToString() == "Scene 4 - Tongue Swelling")
                    {
                        sce2 = currentrow.GetValue("Event_Time")?.ToString();
                    }
                }


                if (check2.Count > 0 && sce != null && DifficultyType != "ADVANCED")
                {
                    foreach (var p in check2)
                    {
                        if (long.Parse(p) > long.Parse(sce))
                        {
                            check1P = true;
                        }
                    }
                }
                else if (check2.Count > 0 && sce2 != null && DifficultyType == "ADVANCED")
                {
                    foreach (var p in check2)
                    {
                        if (long.Parse(p) < long.Parse(sce2) && long.Parse(p) > long.Parse(sce))
                        {
                            check2P = true;
                        }
                    }
                }

                if (!check1P && sce != null)
                {
                    ErrorType = "Critical";
                    Description = "Not checking pulses during scene 2";
                }
                else if (!check2P && sce2 != null)
                {
                    ErrorType = "Critical";
                    Description = "Not checking pulses during scene 2";
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
}

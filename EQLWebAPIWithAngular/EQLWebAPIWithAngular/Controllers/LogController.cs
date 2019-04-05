using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChlaDataRepository;
using DataRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
//using MySQLDataRepository;
using Newtonsoft.Json.Linq;

namespace EQLWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("Log")]
    [Route("api/[controller]/[Action]")]
    public class LogController : Controller
    {

        private readonly IDistributedCache _distributedCache;
        private readonly IResultAnanlyser _resultAnanlyser;

        readonly ILogRepository<ILog> UserRepository;

        public enum SecnarioNameEnum
        {
            Seizure_Status_Epilepticus = 1, Anaphylaxis = 2, Adult_Seizure_Status_Epilepticus = 3
        }


        public LogController(IDistributedCache distributedCache)
        {
            //DataRepository = dr;
            _distributedCache = distributedCache;
            //_resultAnanlyser = resultAnanlyser;
        }

        [HttpPost]
        public async Task<JsonResult> SingleLogData(string t)
        {
            string logData = string.Empty;
            using (var requestBodyStream = new MemoryStream())
            {
                var body = HttpContext.Request.Body;
                await HttpContext.Request.Body.CopyToAsync(requestBodyStream);
                requestBodyStream.Seek(0, SeekOrigin.Begin);
                logData = await new StreamReader(requestBodyStream).ReadToEndAsync();
            }
            if (logData != null && !String.IsNullOrEmpty(logData))
            {

                if (t == "m")
                {
                    var logJson = JObject.Parse(logData);
                    JToken sessionID = null;
                    if (logJson.TryGetValue("Session_ID", out sessionID))
                    {
                        if (sessionID != null && !String.IsNullOrEmpty(sessionID.ToString()))
                        {
                            _distributedCache.SetString(sessionID.ToString() + "-" + "Master", logJson.ToString());
                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                data = "",
                                error = "No data found!!"
                            });
                        }
                        return Json(new
                        {
                            success = true,
                            data = "Data Inserted!!",
                            error = ""
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            data = "",
                            error = "No data found!!"
                        });
                    }
                }
                else if (t == "e")
                {
                    var logJson = JObject.Parse(logData);
                    JToken logSessionID = null;
                    if (logJson.TryGetValue("Session_ID", out logSessionID))
                    {
                        if (logSessionID != null && !String.IsNullOrEmpty(logSessionID.ToString()))
                        {
                            JArray jArray = new JArray();
                            string resData = _distributedCache.GetString(logSessionID.ToString() + "-" + "Events");
                            if (!String.IsNullOrEmpty(resData) && resData != null)
                            {
                                jArray = JArray.Parse(resData);
                            }
                            jArray.Add(logJson);
                            _distributedCache.SetString(logSessionID.ToString() + "-" + "Events", jArray.ToString());

                            if (_distributedCache.GetString(logSessionID.ToString() + "-" + "Master") != null)
                            {
                                var mlog = _distributedCache.GetString(logSessionID.ToString() + "-" + "Master");
                                var elog = logData;
                                AddEventsToDB(mlog, logJson);
                            }

                        }
                        else
                        {
                            return Json(new
                            {
                                success = false,
                                data = "",
                                error = "No data found!!"
                            });
                        }
                        return Json(new
                        {
                            success = true,
                            data = "Data Inserted!!",
                            error = ""
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            data = "",
                            error = "No data found!!"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        success = false,
                        data = "",
                        error = "No data found!!"
                    });
                }

            }
            else
            {
                return Json(new
                {
                    success = false,
                    data = "",
                    error = "No data found!!"
                });
            }
        }

        private void AddEventsToDB(string mlog, JObject elog)
        {
            ILogRepository<ILog> _log = new LogDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            _log.AddNewLog(mlog, elog);
        }

        [HttpGet]
        public async Task<JsonResult> GetResult(string SessionId)
        {
            string resEvent = _distributedCache.GetString(SessionId + "-" + "Events");
            string resMaster = _distributedCache.GetString(SessionId + "-" + "Master");

            if (resEvent != null && resMaster != null)
            {
                ILogRepository<ILog> _log = new LogDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
                var data = await _log.GetDataFromDataBase(SessionId);
                resEvent = data.GetValue("Log").ToString();
                resMaster = data.GetValue("Main").ToString();
            }


            if (resEvent != null && resMaster != null)
            {
                string ScenarioName = "";
                string gDifficulty = "";
                string gUser = "";
                string Date = "0";
                string StartTime = "0";
                string StopTime = "0";

                var jarrEve = JArray.Parse(resEvent);
                if (jarrEve != null)
                {
                    foreach (var jobj in jarrEve)
                    {
                        var currentrow = (JObject)jobj;
                        if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_STARTED")
                        {
                            StartTime = currentrow.GetValue("Event_Time")?.ToString();
                            gDifficulty = currentrow.GetValue("Difficulty")?.ToString();
                            ScenarioName = currentrow.GetValue("Scenario")?.ToString();
                        }
                        else if (currentrow.GetValue("ActionID")?.ToString() == "SCENARIO_ENDED")
                        {
                            StopTime = currentrow.GetValue("Event_Time")?.ToString();
                        }
                    }
                }
                var jObjectMain = JObject.Parse(resMaster);
                if (jObjectMain != null)
                {
                    Date = jObjectMain.GetValue("Event_Time")?.ToString();
                    gUser = jObjectMain.GetValue("userid")?.ToString();
                }

                var mainData = "{" + "'Events'" + ":" + resEvent + "," + "'Master'" + ":" + resMaster + "}";

                var jObject = JObject.Parse(mainData);

                AnalysisFactory analysisFactory = new AnalysisFactory();

                IResultAnanlyser ra = new ResultAnanlyser();

                var analysis = analysisFactory.GetAnalysis(ScenarioName);
                ra.RegisterAnalysis(analysis, "Quantitative");

                var analysis2 = analysisFactory.GetAnalysis(ScenarioName + "_Qualitative");
                ra.RegisterAnalysis(analysis2, "Qualitative");

                var result = ra.PerformAnalysis(jObject);

                GameResultModel gameResult = new GameResultModel();

                Details details = new Details();
                details.Date = Convert.ToInt64(Date);
                details.Difficulty = gDifficulty == "BEGINNER" ? "Standard" : gDifficulty == "ADVANCED" ? "Advanced" : gDifficulty;
                details.Distraction = "High";
                details.Scenario = "Scenario " + (int)(SecnarioNameEnum)Enum.Parse(typeof(SecnarioNameEnum), ScenarioName);
                details.Type = ScenarioName;
                details.User = gUser;

                gameResult.Details = details;

                JObject resQualitative = new JObject();
                resQualitative.Add("Category", "Example Category");
                resQualitative.Add("DifficultyType", "Standard");
                resQualitative.Add("ErrorType", "Mild");
                resQualitative.Add("Description", "Example qualitative data");

                JArray resQualitativeList = new JArray();
                resQualitativeList.Add(resQualitative);



                gameResult.Qualitative = (JArray)result.GetValue("Qualitative"); 
                gameResult.Quantitative = (JArray)result.GetValue("Quantitative");

                return Json(new
                {
                    success = true,
                    data = gameResult,
                    error = ""
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    data = "",
                    error = "No data found!!"
                });
            }
        }


    }
}

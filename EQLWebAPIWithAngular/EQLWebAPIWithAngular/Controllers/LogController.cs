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
using MySQLDataRepository;
using Newtonsoft.Json.Linq;

namespace EQLWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("Log")]
    [Route("api/[controller]/[Action]")]
    public class LogController : Controller
    {
        private IDataRepository<Log> DataRepository;
        private readonly IDistributedCache _distributedCache;

        public LogController(IDistributedCache distributedCache)
        {
            //DataRepository = dr;
            _distributedCache = distributedCache;
        }

        [Route("Stream")]
        // POST: Log/Stream
        [HttpPost]
        public async Task<JsonResult> PostStream(string c)
        {
            string json = string.Empty;


            using (var requestBodyStream = new MemoryStream())
            {
                var body = HttpContext.Request.Body;
                await HttpContext.Request.Body.CopyToAsync(requestBodyStream);
                requestBodyStream.Seek(0, SeekOrigin.Begin);
                json = await new StreamReader(requestBodyStream).ReadToEndAsync();
            }


            if (String.IsNullOrEmpty(c))
            {
                c = "orphan";
            }

            IDataRepository<ILog> ilg = new LogDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=106.51.76.45;Database=chladb;Uid=root;Pwd=P@ss1ord;" });


            ilg.Add(new Log() { LogData = json }, c);


            return Json(new { Result = "OK" });
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

        [HttpGet]
        public async Task<JsonResult> GetResult(string SessionId)
        {

            GameResultModel gameResult = new GameResultModel();

            Details details = new Details();
            details.Date = 154334344;
            details.Difficulty = "High";
            details.Distraction = "High";
            details.Scenario = "Scenario 3";
            details.Type = "Adult Seizure Status Epilepticus";
            details.User = "jackryan";

            gameResult.Details = details;

            List<ResultView> resultViews = new List<ResultView>();
            resultViews.Add(new ResultView { DisplayTitle = "Time To Suction", DisplayValue = "11:00" });
            resultViews.Add(new ResultView { DisplayTitle = "Time To Intubation From Scene 5", DisplayValue = "12:00" });

            gameResult.Qualitative = resultViews;
            gameResult.Quantitative = resultViews;


            return Json(new
            {
                success = true,
                data = gameResult,
                error = ""
            });
        }


    }
}

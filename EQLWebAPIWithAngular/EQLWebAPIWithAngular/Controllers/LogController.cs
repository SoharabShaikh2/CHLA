using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
            if (logData != null)
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
                    }
                }
                if (t == "e")
                {
                    var logJson = JObject.Parse(logData);
                    JToken logSessionID = null;
                    if (logJson.TryGetValue("Session_ID", out logSessionID))
                    {
                        if (logSessionID != null && !String.IsNullOrEmpty(logSessionID.ToString()))
                        {
                            JArray jArray = new JArray();
                            string resData = _distributedCache.GetString(logSessionID.ToString() + "-" + "Events");
                            if(!String.IsNullOrEmpty(resData) && resData != null)
                            {
                                jArray = JArray.Parse(resData);
                            }                           
                            jArray.Add(logJson);
                            _distributedCache.SetString(logSessionID.ToString() + "-" + "Events", jArray.ToString());
                        }
                    }
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


    }
}

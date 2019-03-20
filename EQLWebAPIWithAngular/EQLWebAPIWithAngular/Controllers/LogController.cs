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
        public async Task<JsonResult> SingleLogData([FromBody]string masterData)
        {
            if (masterData != null)
            {
                var logJson = JObject.Parse(masterData);
                JToken mainData = null;
                if (logJson.TryGetValue("projectname", out mainData))
                {
                    JToken sessionID = null;
                    if (mainData != null && !String.IsNullOrEmpty(mainData.ToString()))
                    {
                        if (logJson.TryGetValue("Session_ID", out sessionID))
                        {
                            if (sessionID != null && !String.IsNullOrEmpty(sessionID.ToString()))
                            {
                                _distributedCache.SetString(sessionID.ToString(), masterData);
                            }
                        }
                    }
                }

                JToken logData = null;
                if (logJson.TryGetValue("ActionID", out logData))
                {
                    JToken logSessionID = null;
                    if (logData != null && !String.IsNullOrEmpty(logData.ToString()))
                    {
                        if (logJson.TryGetValue("Session_ID", out logSessionID))
                        {
                            if (logSessionID != null && !String.IsNullOrEmpty(logSessionID.ToString()))
                            {
                                _distributedCache.SetString(logSessionID.ToString(), masterData);
                            }
                        }
                    }
                }

                return Json(new
                {
                    status = true,
                    data = "Data Inserted!!",
                    error = ""
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    data = "",
                    error = "No data found!!"
                });
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySQLDataRepository;
using Newtonsoft.Json.Linq;

namespace EQLWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("Log")]
    public class LogController : Controller
    {
		IDataRepository<Log> DataRepository;

		//public LogController(IDataRepository<Log> dr)
		//{
		//	DataRepository = dr;
		//}
        // GET: api/Log
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Log/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

		
		[Route("Stream")]
		// POST: Log/Stream
		[HttpPost]
        public async Task< JsonResult> PostStream(string c)
        {
            string json=string.Empty;
          
                
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
            
            IDataRepository<ILog> ilg = new MySQLDataRepository.LogDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=106.51.76.45;Database=chladb;Uid=root;Pwd=P@ss1ord;" });
          

            ilg.Add(new Log() { LogData = json }, c);
          

			return Json(new {  Result="OK" });
        }

		[Route("Chunk")]
		// POST: Log/Chunk
		[HttpPost]
		public void PostChunk([FromBody]string value)
		{
		}

		[Route("Chunk/Master")]
		// POST: Log/Chunk/Master
		[HttpPost]
		public void PostChunkMaster([FromBody]string value)
		{
		}
		// PUT: api/Log/5
		[HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

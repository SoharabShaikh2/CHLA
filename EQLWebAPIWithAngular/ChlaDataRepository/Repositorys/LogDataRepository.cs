using DataRepository;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChlaDataRepository
{
    public class LogDataRepository : ILogRepository<ILog>
    {
        private IConnectionParameters connectionParameters;
        public LogDataRepository(IConnectionParameters conParam)
        {
            connectionParameters = conParam;
        }

        public void AddNewLog(string mainLog, JObject eveLog)
        {
            String SQL = "Insert into logs(log,mlog) Values (@log,@mainlog)";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                var mjobj = JObject.Parse(mainLog);

                //var jobj2 = JObject.Parse(eveLog);

                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@log", mjobj.ToString());
                cmd.Parameters.AddWithValue("@mainlog", eveLog.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
        }

        public async Task<JObject> GetDataFromDataBase(string sessionId)
        {
            JObject full = new JObject();
            JArray jArray = new JArray();
            JObject main = new JObject();
            String SQL = "SELECT log,mlog FROM chlaanalytics.logs where Session_ID=@sId;";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@sId", sessionId);
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        jArray.Add(JObject.Parse(Convert.ToString(reader["mlog"])));
                        main = JObject.Parse(Convert.ToString(reader["log"]));
                    }
                }
                full.Add("Main", main);
                full.Add("Log", jArray);

                return full;
            }


        }
    }
}

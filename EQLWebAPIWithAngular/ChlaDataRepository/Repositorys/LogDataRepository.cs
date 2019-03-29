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
    }
}

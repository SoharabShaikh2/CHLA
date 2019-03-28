using DataRepository;
using MySql.Data.MySqlClient;
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

        public void AddNewLog(string mainLog, string eveLog)
        {
            String SQL = "Insert into logs(log,mlog) Values ('@log','@mainlog')";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@log",  eveLog);
                cmd.Parameters.AddWithValue("@mainlog", mainLog);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
        }
    }
}

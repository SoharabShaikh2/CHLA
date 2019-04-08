using DataRepository;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChlaDataRepository
{
    public class ResultDataRepository : IResultRepository<IResult>
    {
        private IConnectionParameters connectionParameters;
        public ResultDataRepository(IConnectionParameters conParam)
        {
            connectionParameters = conParam;
        }
        public async Task<List<ResultDto>> GetResults(string userId)
        {
            List<ResultDto> results = new List<ResultDto>();
            String SQL = "SELECT * FROM results where userid like @userId";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@userId", "%" + userId + "%");
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        ResultDto result = new ResultDto();
                        result.id = Convert.ToInt32(reader["id"]);
                        result.DateTimeSession = (Convert.ToDateTime(reader["DateTimeSession"])).ToString("F");
                        result.ResultJSon = Convert.ToString(reader["ResultJSon"]);
                        result.scenarioname = Convert.ToString(reader["scenarioname"]);
                        result.userid = Convert.ToString(reader["userid"]);
                        results.Add(result);
                    }
                }
                return results;
            }
        }

        public void AddNewResult(string userId, JObject result, string scenarioname, DateTime dateSession)
        {
            String SQL = "insert into results(userid,scenarioname,DateTimeSession,ResultJSon) values(@userId,@sceName,@Date,@res)";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {

                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@sceName", scenarioname);
                cmd.Parameters.AddWithValue("@Date", dateSession);
                cmd.Parameters.AddWithValue("@res", result.ToString());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
        }
    }
}

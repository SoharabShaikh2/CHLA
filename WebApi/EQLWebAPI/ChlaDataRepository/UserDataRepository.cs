using DataRepository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChlaDataRepository
{
    public class UserDataRepository : IUserRepository<IUser>
    {
        private IConnectionParameters connectionParameters;
        public UserDataRepository(IConnectionParameters conParam)
        {
            connectionParameters = conParam;
        }

        public int Add(IUser data, string collectionName)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> LoginUser(string email, string password)
        {
            UserDto user = new UserDto();
            String SQL = "select * from chla.user where email= @email && password= @password;";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        user.id = Convert.ToInt32(reader["id"]);
                        user.usertypeid = Convert.ToInt32(reader["usertypeid"]);
                        user.organizationid = Convert.ToInt32(Convert.ToString(reader["organizationid"]) ==""?"0": Convert.ToString(reader["organizationid"]));
                    }
                }
                return user;
            }
        }
    }
}

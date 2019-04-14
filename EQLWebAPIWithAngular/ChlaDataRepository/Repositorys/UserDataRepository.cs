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
            String SQL = "select u.firstname, u.lastname, u.username, ut.type as usertype, o.name as organizationname from user u join usertype ut on u.usertypeid = ut.id left join organization o on u.organizationid = o.id where username= @email && password= @password;";
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
                        user.firstname = Convert.ToString(reader["firstname"]);
                        user.lastname = Convert.ToString(reader["lastname"]);
                        user.organizationname = Convert.ToString(reader["organizationname"]);
                        user.usertype = Convert.ToString(reader["usertype"]);
                        user.username = Convert.ToString(reader["username"]);
                    }
                }
                return user;
            }

        }

        public async Task<UserDtoApp> LoginUserApp(string email, string password)
        {
            UserDtoApp user = new UserDtoApp();
            String SQL = "select * from user u join organization o on u.organizationid = o.id where username= @email && password= @password;";
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
                        user.username = Convert.ToString(reader["username"]);
                        user.firstname = Convert.ToString(reader["firstname"]);
                        user.lastname = Convert.ToString(reader["lastname"]);
                        user.organizationName = Convert.ToString(reader["name"]);
                        user.organizationid = Convert.ToInt32(Convert.ToString(reader["organizationid"]) == "" ? "0" : Convert.ToString(reader["organizationid"]));
                    }
                }
                return user;
            }

        }

        public async Task<UserDtoApp> LoginUserAppFromAdmin(int UserId)
        {
            UserDtoApp user = new UserDtoApp();
            String SQL = "select * from user u join organization o on u.organizationid = o.id where u.id = @userId;";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@userId", UserId);
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        user.id = Convert.ToInt32(reader["id"]);
                        user.usertypeid = Convert.ToInt32(reader["usertypeid"]);
                        user.username = Convert.ToString(reader["username"]);
                        user.firstname = Convert.ToString(reader["firstname"]);
                        user.lastname = Convert.ToString(reader["lastname"]);
                        user.organizationName = Convert.ToString(reader["name"]);
                        user.organizationid = Convert.ToInt32(Convert.ToString(reader["organizationid"]) == "" ? "0" : Convert.ToString(reader["organizationid"]));
                    }
                }
                return user;
            }

        }

    }
}

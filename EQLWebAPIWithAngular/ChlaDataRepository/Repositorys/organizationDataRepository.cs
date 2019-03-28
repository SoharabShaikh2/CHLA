using DataRepository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChlaDataRepository
{
    public class OrganizationDataRepository : IOrganizationRepository<IOrganization>
    {

        private IConnectionParameters connectionParameters;
        public OrganizationDataRepository(IConnectionParameters conParam)
        {
            connectionParameters = conParam;
        }

        public async Task<OrganizationDto> GetOrganization(int organizationId)
        {
            OrganizationDto dto = new OrganizationDto();
            String SQL = "SELECT * FROM organization where id = @id;";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@id", organizationId);
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        dto.id = Convert.ToInt32(reader["id"]);
                        dto.name = Convert.ToString(reader["name"]);
                        dto.address = Convert.ToString(reader["address"]);
                        dto.contactemail = Convert.ToString(reader["contactemail"]);
                        dto.contactno = Convert.ToString(reader["contactno"]);
                        dto.contactperson = Convert.ToString(reader["contactperson"]);
                        dto.expiry = Convert.ToDateTime(reader["expiry"].ToString() == "" ? null : reader["expiry"]);
                        dto.isactive = Convert.ToBoolean(reader["isactive"].ToString() == "" ? true : reader["isactive"]);
                        dto.registeredon = Convert.ToDateTime(reader["registeredon"].ToString() == "" ? null : reader["registeredon"]);
                        dto.timezone_mins = Convert.ToInt32(reader["timezone_mins"].ToString() == "" ? null : reader["timezone_mins"]);
                    }
                }
                return dto;
            }
        }

        public async Task<List<OrganizationDto>> GetOrganizationList()
        {
            List<OrganizationDto> list = new List<OrganizationDto>();
           
            String SQL = "SELECT o.id, o.name, o.isactive ,SUM(CASE WHEN u.userTypeid = 3 THEN 1 ELSE 0 END) as totalUser,SUM(CASE WHEN u.userTypeid = 2 THEN 1 ELSE 0 END) as totalAdmin FROM organization o left join user u on o.id = u.organizationid group by o.name";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        OrganizationDto dto = new OrganizationDto();
                        dto.id = Convert.ToInt32(reader["id"]);
                        dto.name = Convert.ToString(reader["name"]);
                        //dto.address = Convert.ToString(reader["address"]);
                        //dto.contactemail = Convert.ToString(reader["contactemail"]);
                        //dto.contactno = Convert.ToString(reader["contactno"]);
                        //dto.contactperson = Convert.ToString(reader["contactperson"]);
                        //dto.expiry = Convert.ToDateTime(reader["expiry"].ToString() == "" ? null : reader["expiry"]);
                        dto.isactive = Convert.ToBoolean(reader["isactive"].ToString() == "" ? true : reader["isactive"]);
                        //dto.registeredon = Convert.ToDateTime(reader["registeredon"].ToString() == "" ? null : reader["registeredon"]);
                        //dto.timezone_mins = Convert.ToInt32(reader["timezone_mins"].ToString() == "" ? null : reader["timezone_mins"]);
                        dto.totalUser = Convert.ToInt32(reader["totalUser"].ToString());
                        dto.totalAdmin = Convert.ToInt32(reader["totalAdmin"].ToString());

                        list.Add(dto);
                    }
                }
                return list;
            }
        }

        public async Task<List<OrganizationUserDto>> GetOrganizationUsers(int organizationId)
        {
            List<OrganizationUserDto> list = new List<OrganizationUserDto>();

            String SQL = "SELECT id, CONCAT(firstname,' ',lastname) as fullname, usertypeid FROM user where organizationid = @id";
            using (MySqlConnection con = new MySqlConnection(connectionParameters.ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(SQL, con);
                cmd.Parameters.AddWithValue("@id", organizationId);
                con.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        OrganizationUserDto dto = new OrganizationUserDto();
                        dto.id = Convert.ToInt32(reader["id"]);
                        dto.fullname = Convert.ToString(reader["fullname"]);
                        dto.usertypeid = Convert.ToInt32(reader["usertypeid"].ToString());
                        list.Add(dto);
                    }
                }
                return list;
            }
        }
    }
}

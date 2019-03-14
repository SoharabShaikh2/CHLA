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
            String SQL = "SELECT * FROM chla.organization where id = @id;";
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
    }
}

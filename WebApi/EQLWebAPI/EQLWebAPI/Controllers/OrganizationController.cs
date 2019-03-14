using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChlaDataRepository;
using DataRepository;
using Microsoft.AspNetCore.Mvc;
using MySQLDataRepository;

namespace EQLWebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class OrganizationController : Controller
    {
        IOrganizationRepository<IOrganization> OrganizationRepository;

        // GET api/values
        [HttpGet]
        public async Task<OrganizationDto> OrganizationLogin(string emailId, string password)
        {
            OrganizationDto organization = new OrganizationDto();
            IUserRepository<IUser> _user = new UserDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=localhost;Database=chla;Uid=root;Pwd=123456;" });
            var res = await _user.LoginUser(emailId, password);

            if(res.usertypeid == 2)
            {
              IOrganizationRepository<IOrganization> _organization = new OrganizationDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=localhost;Database=chla;Uid=root;Pwd=123456;" });
                organization = await _organization.GetOrganization(res.organizationid);
            }

            return organization;
        }


    }
}

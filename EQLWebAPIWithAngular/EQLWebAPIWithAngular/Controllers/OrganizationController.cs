using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChlaDataRepository;
using DataRepository;
using EQLWebAPIWithAngular.Models;
using Microsoft.AspNetCore.Mvc;
//using MySQLDataRepository;

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
            var res = await _user.LoginUserApp(emailId, password);

            if (res.usertypeid == 2)
            {
                IOrganizationRepository<IOrganization> _organization = new OrganizationDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=localhost;Database=chla;Uid=root;Pwd=123456;" });
                organization = await _organization.GetOrganization(res.organizationid);
            }

            return organization;
        }

        [HttpGet]
        public async Task<List<OrganizationDto>> OrganizationList()
        {
            List<OrganizationDto> organizationList = new List<OrganizationDto>();
            IOrganizationRepository<IOrganization> _organization = new OrganizationDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            organizationList = await _organization.GetOrganizationList();
            return organizationList;
        }

        [HttpPost]
        public async Task<List<OrganizationUserDto>> OrganizationUsersList([FromBody]int id)
        {
            List<OrganizationUserDto> organizationUsers = new List<OrganizationUserDto>();
            IOrganizationRepository<IOrganization> _organization = new OrganizationDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            organizationUsers = await _organization.GetOrganizationUsers(id);
            return organizationUsers;
        }

        [HttpPost]
        public async Task<List<OrganizationUserDto>> OrganizationUsersListSearch([FromBody]SearchUserDto find)
        {
            List<OrganizationUserDto> organizationUsers = new List<OrganizationUserDto>();
            IOrganizationRepository<IOrganization> _organization = new OrganizationDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            organizationUsers = await _organization.GetOrganizationUsersSearch(find.id, find.text);
            return organizationUsers;
        }

        [HttpPost]
        public async Task<List<ResultDto>> GetUserResult([FromBody]SearchUserDto input)
        {
            List<ResultDto> userResult = new List<ResultDto>();
            IResultRepository<IResult> _res = new ResultDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            userResult =await _res.GetResults(input.text,input.input,input.dateTime);
            return userResult;
        }

        [HttpPost]
        public async Task<List<ResultDto>> GetUserResultDates([FromBody]SearchUserDto input)
        {
            List<ResultDto> userResult = new List<ResultDto>();
            IResultRepository<IResult> _res = new ResultDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            userResult = await _res.GetDateForUserID(input.text);
            return userResult;
        }
    }
}

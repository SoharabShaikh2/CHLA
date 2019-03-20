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
    public class UserController : Controller
    {
        IUserRepository<IUser> UserRepository;

        // GET api/values
        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody]UserLogin login)
        {
            UserDto user = new UserDto();
            IUserRepository<IUser> _user = new UserDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            user = await _user.LoginUser(login.Username, login.Password);

            if(user.id > 0)
            {
                return Json(new
                {
                    status = true,
                    data = user,
                    error = ""
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    data = "",
                    error = "Incorrect Username or Password!!!"
                });   
            }
        }

        


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChlaDataRepository;
using DataRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using MySQLDataRepository;

namespace EQLWebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class UserController : Controller
    {
        readonly IUserRepository<IUser> UserRepository;

        // GET api/values
        [HttpPost]
        public async Task<IActionResult> UserLogin([FromBody]UserLogin login)
        {
            UserDto user = new UserDto();
            IUserRepository<IUser> _user = new UserDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            user = await _user.LoginUser(login.Username, login.Password);

            if (!String.IsNullOrEmpty(user.username))
            {
                return Json(new
                {
                    success = true,
                    data = user,
                    error = ""
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    data = "",
                    error = "Incorrect Username or Password"
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserLoginApp([FromBody]UserLogin login)
        {
            UserDtoApp user = new UserDtoApp();
            IUserRepository<IUser> _user = new UserDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
            user = await _user.LoginUserApp(login.Username, login.Password);

            if (user.id > 0)
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
                    error = "Incorrect Username or Password"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserLoginAppFromAdmin()
        {
            UserLogin login = new UserLogin();
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                login.UserId = int.Parse(HttpContext.Session.GetString("userId"));
                UserDtoApp user = new UserDtoApp();
                IUserRepository<IUser> _user = new UserDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;" });
                user = await _user.LoginUserAppFromAdmin(login.UserId);
                if (user.id > 0)
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
                        error = "Incorrect Username or Password"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = false,
                    data = "",
                    error = "Incorrect Username or Password"
                });
            }


        }




    }
}

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
        public async Task<UserDto> UserLogin([FromBody]UserLogin login)
        {
            UserDto user = new UserDto();
            IUserRepository<IUser> _user = new UserDataRepository(new MySqlConnectionParameters() { ConnectionString = "Server=localhost;Database=chla;Uid=root;Pwd=123456;" });
            user = await _user.LoginUser(login.Username, login.Password);

            return user;
        }

        


    }
}

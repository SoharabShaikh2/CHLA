using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ChlaDataRepository;
using DataRepository;
using EQLWebAPIWithAngular.DatabaseContext;
using EQLWebAPIWithAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using MySQLDataRepository;

namespace EQLWebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class UserController : Controller
    {
        readonly IUserRepository<IUser> UserRepository;
        private readonly MySqlDbContext _context;

        public UserController(MySqlDbContext context)
        {
            _context = context;
        }

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

        [HttpPost]
        public async Task<IActionResult> PasswordReset([FromBody]UserLogin login)
        {
            var user = _context.User.Where(x => x.UserName == login.Username).FirstOrDefault();

            if (user != null)
            {
                Random generator = new Random();
                String r = generator.Next(0, 999999).ToString("D6");
                PasswordReset dto = new PasswordReset();
                dto.Resetcode = int.Parse(r);
                dto.userid = user.Id;
                dto.status = false;
                await _context.PasswordReset.AddAsync(dto);
                await _context.SaveChangesAsync();

                await SendEmailAsync(user.Email, r);
                return Json(new
                {
                    status = true,
                    data = "",
                    error = ""
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    data = "",
                    error = "Incorrect Username"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetNewPassword([FromBody]PasswordSet pass)
        {
            if (pass != null)
            {
                var passDto = _context.PasswordReset.Where(x => x.Resetcode == pass.ResetCode).FirstOrDefault();

                if (passDto != null && !passDto.status)
                {
                    var userSet = _context.User.Where(x => x.Id == passDto.userid).FirstOrDefault();
                    if (userSet != null)
                    {
                        userSet.Password = pass.Password;
                        _context.User.Update(userSet);

                        passDto.status = true;
                        _context.PasswordReset.Update(passDto);
                        await _context.SaveChangesAsync();

                        return Json(new
                        {
                            status = true,
                            data = "",
                            error = ""
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            status = false,
                            data = "",
                            error = "Reset not valid"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        status = false,
                        data = "",
                        error = "Incorrect Reset code"
                    });
                }
            }
            else
            {
                return Json(new
                {
                    status = false,
                    data = "",
                    error = "Incorrect Reset code"
                });
            }
        }

        private Task SendEmailAsync(string email, string resetCode)
        {
            try
            {
                string subject = "Reset Password";
                string htmlMessage = "<html><body><h3>Your password reset code is:</h3><h3 style=\"color:#312970\">" + resetCode + "</h3>></body></html>";

                var client = new SmtpClient("smtp.gmail.com")
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("iamsoharab@gmail.com", "Soharab143Sab#"),
                    Port = 587
                };
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("resuscitation-vr-noreply@aisolve.com")
                };
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessage;
                return client.SendMailAsync(mailMessage);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}

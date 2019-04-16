using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EQLWebAPIWithAngular.DatabaseContext;
using EQLWebAPIWithAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EQLWebAPIWithAngular.Controllers
{
    public class AdminLoginController : Controller
    {


        private readonly MySqlDbContext _context;

        public AdminLoginController(MySqlDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new Login());
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View("Index", new Login());
        }

        [HttpPost]
        public IActionResult Login([FromForm]Models.Login login)
        {

            var reset = _context.PasswordReset.ToList();
            var users = _context.User.Where(u => u.UserName == login.UserName && u.Password == login.Password).Include(u => u.Organization).Include(u => u.UserType);
            User user;
            if (users.Count() > 0 && (user = users.First()).UserType.Type != "user")
            {
                HttpContext.Session.SetString("utype", user.UserType.Type);
                HttpContext.Session.SetString("orgid", user.Organization.Id.ToString());
                HttpContext.Session.SetString("userId", user.Id.ToString());

                return RedirectToAction("Index", "UserManagement");
            }

            login.Message = "Incorrect Username or Password";


            return View("Index", login);
        }
    }
}
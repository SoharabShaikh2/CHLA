using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EQLWebAPIWithAngular.DatabaseContext;
using EQLWebAPIWithAngular.Models;
using Microsoft.AspNetCore.Http;

namespace EQLWebAPIWithAngular.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly MySqlDbContext _context;

        public UserManagementController(MySqlDbContext context)
        {
            _context = context;
        }

        // GET: UserManagement
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("utype") == "*")
            {
                var mySqlDbContext = _context.User.Include(u => u.Organization).Include(u => u.UserType);
                return View(await mySqlDbContext.ToListAsync());
            }
            else if (HttpContext.Session.GetString("utype") == "admin")
            {
                var mySqlDbContext = _context.User.Where(u => u.OrganizationId.ToString() == HttpContext.Session.GetString("orgid")).Include(u => u.Organization).Include(u => u.UserType);
                return View(await mySqlDbContext.ToListAsync());
            }
            else
            {
                //await Task.Run(()=> Console.WriteLine());
                return Redirect("~/");
            }
        }

        // GET: UserManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Organization)
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UserManagement/Create
        public IActionResult Create()
        {
            ViewBag.userNameExist = false;
            if (HttpContext.Session.GetString("utype") == "*")
            {

                ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Name");
                ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(u => u.Type != "*"), "Id", "Type");
            }
            else if (HttpContext.Session.GetString("utype") == "admin")
            {
                ViewData["OrganizationId"] = new SelectList(_context.Organization.Where(o => o.Id.ToString() == HttpContext.Session.GetString("orgid")), "Id", "Name");
                ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(u => u.Type == "user"), "Id", "Type");
            }
            else
            {
                //await Task.Run(()=> Console.WriteLine());
                return Redirect("~/");
            }
            return View();
        }

        // POST: UserManagement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,UserName,Password,Email,Expiry,IsActive,OrganizationId,UserTypeId")] User user)
        {
            var userExist = await _context.User.Where(x => x.UserName == user.UserName).ToListAsync();

            if (userExist.Count > 0)
            {
                ViewBag.userNameExist = true;
                //return View(user);
            }
            else if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            if (HttpContext.Session.GetString("utype") == "*")
            {

                ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Name", user.OrganizationId);
                ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(u => u.Type != "*"), "Id", "Type", user.UserTypeId);
            }
            else if (HttpContext.Session.GetString("utype") == "admin")
            {
                ViewData["OrganizationId"] = new SelectList(_context.Organization.Where(o => o.Id.ToString() == HttpContext.Session.GetString("orgid")), "Id", "Name", user.OrganizationId);
                ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(u => u.Type == "user"), "Id", "Type", user.UserTypeId);
            }
            else
            {
                //await Task.Run(()=> Console.WriteLine());
                return Redirect("~/");
            }

            //ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Name", user.OrganizationId);
            //ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "Type", user.UserTypeId);
            return View(user);
        }

        // GET: UserManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.userNameExist = false;

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }


            if (HttpContext.Session.GetString("utype") == "*")
            {

                ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Name", user.OrganizationId);
                ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(u => u.Type != "*"), "Id", "Type", user.UserTypeId);
            }
            else if (HttpContext.Session.GetString("utype") == "admin")
            {
                ViewData["OrganizationId"] = new SelectList(_context.Organization.Where(o => o.Id.ToString() == HttpContext.Session.GetString("orgid")), "Id", "Name", user.OrganizationId);
                ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(u => u.Type == "user"), "Id", "Type", user.UserTypeId);
            }
            else
            {
                //await Task.Run(()=> Console.WriteLine());
                return Redirect("~/");
            }
            //ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Name", user.OrganizationId);
            //ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "Type", user.UserTypeId);
            return View(user);
        }

        // POST: UserManagement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,UserName,Password,Email,Expiry,IsActive,OrganizationId,UserTypeId")] User user)
        {
            if (id != user.Id)
            {
                // user.Id = id;
                return NotFound();
            }
            var userExist = await _context.User.Where(x => x.UserName == user.UserName && x.Id != user.Id).ToListAsync();
            var userMain = await _context.User.Where(x => x.Id == user.Id).FirstOrDefaultAsync();

            if (userMain.UserTypeId == 4)
            {
                user.UserTypeId = 4;
            }

            userMain.Email = user.Email;
            userMain.Expiry = user.Expiry;
            userMain.FirstName = user.FirstName;
            userMain.IsActive = user.IsActive;
            userMain.LastName = user.LastName;
            userMain.Organization = user.Organization;
            userMain.OrganizationId = user.OrganizationId;
            userMain.Password = user.Password;
            userMain.UserName = user.UserName;
            userMain.UserType = user.UserType;
            userMain.UserTypeId = user.UserTypeId;

            if (userExist.Count > 0)
            {
                ViewBag.userNameExist = true;
            }
            else if (ModelState.IsValid)
            {
                try
                {                             
                    _context.Update(userMain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            if (HttpContext.Session.GetString("utype") == "*")
            {

                ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Name", user.OrganizationId);
                ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(u => u.Type != "*"), "Id", "Type", user.UserTypeId);
            }
            else if (HttpContext.Session.GetString("utype") == "admin")
            {
                ViewData["OrganizationId"] = new SelectList(_context.Organization.Where(o => o.Id.ToString() == HttpContext.Session.GetString("orgid")), "Id", "Name", user.OrganizationId);
                ViewData["UserTypeId"] = new SelectList(_context.UserType.Where(u => u.Type == "user"), "Id", "Type", user.UserTypeId);
            }
            else
            {
                //await Task.Run(()=> Console.WriteLine());
                return Redirect("~/");
            }

            //ViewData["OrganizationId"] = new SelectList(_context.Organization, "Id", "Name", user.OrganizationId);
            //ViewData["UserTypeId"] = new SelectList(_context.UserType, "Id", "Type", user.UserTypeId);
            return View(user);
        }

        // GET: UserManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Organization)
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}

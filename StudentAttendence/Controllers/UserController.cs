using Microsoft.AspNet.Identity.Owin;
using StudentAttendence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentAttendence.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;


        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }


        // GET: User
        public ActionResult Indexs()
        {
            UserModel vm = new UserModel();

            return View();
        }

        
        ApplicationDbContext dbCon = new ApplicationDbContext();
        public async Task<ActionResult> Create()
        {
            UserModel vm = new UserModel();

            var Users = await dbCon.Users.ToListAsync();
            var roles = await dbCon.Roles.ToListAsync();
            ViewBag.UserID = new SelectList(Users, "Id", "UserName");
            ViewBag.RoleID = new SelectList(roles, "Id", "Name");
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserModel model)
        {
            var role = dbCon.Roles.Find(model.RoleID);
            if (role != null)
            {
                await UserManager.AddToRoleAsync(model.UserID, role.Name);
            }
            return RedirectToAction("Create");

        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
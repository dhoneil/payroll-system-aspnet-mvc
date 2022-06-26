using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mjl.Models.Database;

using mjl.Models;
using System.Configuration;
using System.Web.Security;
using Newtonsoft.Json;



namespace mjl.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User model)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            var user = db.Users.Where(u => (u.UserName == model.Email || u.Email == model.Email) && u.Password == model.Password && u.IsActive == true).FirstOrDefault();

            if (user != null)
            {

                int user_id = user.UserID;
                string name = user.FirstName.ToString() + " " + user.LastName.ToString();
                string firstname = user.FirstName.ToString();
                string lastname = user.LastName.ToString();
                string username = user.UserName.ToString();

                Session["user_id"] = user_id.ToString();
                Session["name"] = name;

                return RedirectToAction("Index", "DashBoard");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username and/or password");
            }
            return PartialView(model);
        }

        public ActionResult UserProfile()
        {
            int id = Convert.ToInt32(sysSession.UserID);

            User user = UserModel.gerUserById(id);      
      
            ViewBag.data = user;

            return View();
        }
        public ActionResult EditProfile(User user, string confirmpass)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            User src = new User();
            Employee emp = new Employee();

            emp.EmployeeID = user.UserID;
            emp.FirstName = user.FirstName;
            emp.LastName = user.LastName;
            emp.email = user.Email;

            src.UserID = user.UserID;
            src.FirstName = user.FirstName;
            src.LastName = user.LastName;
            src.UserName = user.UserName;
            src.Email = user.Email;
            src.Password = user.Password;

            if (user.Password != confirmpass)
                return Json(new { message = "<i class='fa fa-exclamation-triangle'></i> Passwords don't match" });
            else
                UserModel.editprofile(src,emp);
                return Json(new { message = "<span style='color:green;'><i class='fa fa-check'></i> Successfully Edited</span>" });
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mjl.Models;
using mjl.Models.Database;

namespace mjl.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public ActionResult Index()
        {
            return View();
        }

        public void CheckSessions()
        {
            if (String.IsNullOrEmpty(sysSession.UserID))
            {
                Response.Write(0);
            }
            else
            {
                Response.Write(1);
            }
        }

        public ActionResult SessionLogin()
        {


            return PartialView();
        }

        public ActionResult Login(User model)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            var user = db.Users.Where(u => (u.UserName == model.UserName || u.Email == model.UserName) && u.Password == model.Password && u.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                int user_id = user.UserID;
                string name = user.FirstName.ToString() + " " + user.LastName.ToString();

                Session["user_id"] = user_id.ToString();
                Session["name"] = name;

                return Json(new { remarks = "success" }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { remarks = "Your username and password incorrect." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
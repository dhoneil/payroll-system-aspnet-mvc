using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using mjl.Models.Filter; 

namespace mjl.Controllers
{
    public class FilterController : Controller
    {
        // GET: Filter
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult getEmployee(int company_id, int job_id)
        {
            var employeelst = FilterModel.getEmployee(company_id, job_id).Select(s => new { name = s.LastName + "," + s.FirstName, emp_id = s.EmployeeID });
            return Json(employeelst, JsonRequestBehavior.AllowGet);
        }
      
    }
}
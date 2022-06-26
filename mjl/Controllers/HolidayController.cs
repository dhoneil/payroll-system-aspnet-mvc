using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using mjl.Models.Database;
using mjl.Models.Filter;
using mjl.Models;

namespace mjl.Controllers
{
    public class HolidayController : Controller
    {
        // GET: Holiday
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewCompanyHoliday()
        {
            List<Company> companylst = FilterModel.getCompany();
            List<Company> companylstUpate = FilterModel.getCompany();
            ViewBag.companyedit = new SelectList(companylstUpate.OrderBy(s => s.company_id), "company_id", "Name");


            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ ALL COMPANY ]]"
            });
            ViewBag.company = new SelectList(companylst.OrderBy(s => s.company_id), "company_id", "Name");
            return View();
        }

        public ActionResult _ViewCompanyHoliday(string search_key, bool status, bool onthisyear)
        {

            List<viewCompanyHoliday> data = HolidayModel.GetData(search_key, status, onthisyear);
            ViewBag.list = data;

            return PartialView();
        }

        public ActionResult updateCompanyHoliday(CompanyHoliday data)
        {
            CompanyHoliday companyHolidayData = new CompanyHoliday();
            companyHolidayData.HolidayID = data.HolidayID;
            companyHolidayData.HolidayName = data.HolidayName;
            companyHolidayData.HolidayType = data.HolidayType;
            companyHolidayData.HolidayDate = data.HolidayDate;
            companyHolidayData.CompanyID = data.CompanyID;
            companyHolidayData.last_edit_by = Convert.ToInt32(sysSession.UserID);
            companyHolidayData.last_edit_date = DateTime.Now;
            companyHolidayData.IsActive = data.IsActive;


            HolidayModel.update(companyHolidayData);
            return Json(true);
        }

        public ActionResult insertCompanyHoliday(CompanyHoliday data)
        {
            if (data.CompanyID == 0)
            {
                //ALl Company
                List<Company> listCompanyActive = CompanyModel.GetAllDataByStatus(true);
                foreach (var items in listCompanyActive)
                {
                    CompanyHoliday companyHolidayData = new CompanyHoliday();
                    companyHolidayData.HolidayName = data.HolidayName;
                    companyHolidayData.HolidayType = data.HolidayType;
                    companyHolidayData.HolidayDate = data.HolidayDate;
                    companyHolidayData.CompanyID = items.company_id;
                    companyHolidayData.prepared_by = Convert.ToInt32(sysSession.UserID);
                    companyHolidayData.prepared_date = DateTime.Now;
                    companyHolidayData.IsActive = true;

                    HolidayModel.insert(companyHolidayData);
                }
            }
            else {
                //Single Company
                CompanyHoliday companyHolidayData = new CompanyHoliday();
                companyHolidayData.HolidayName = data.HolidayName;
                companyHolidayData.HolidayType = data.HolidayType;
                companyHolidayData.HolidayDate = data.HolidayDate;
                companyHolidayData.CompanyID = data.CompanyID;
                companyHolidayData.prepared_by = Convert.ToInt32(sysSession.UserID);
                companyHolidayData.prepared_date = DateTime.Now;
                companyHolidayData.IsActive = true;

                HolidayModel.insert(companyHolidayData);
            }
            return Json(true);
        }
   
    }
}
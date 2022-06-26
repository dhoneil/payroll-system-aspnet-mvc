using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mjl.Models.Database;

namespace mjl.Models
{
    public class HolidayModel
    {
        public static List<viewCompanyHoliday> GetData(string searchkey, bool? is_active, bool? onlythisyear)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            DateTime first_day_of_month = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime last_day_of_month = new DateTime(DateTime.Now.Year, 12, 31);

            IQueryable<viewCompanyHoliday> src = db.viewCompanyHolidays;

            if (!String.IsNullOrEmpty(searchkey)) { src = src.Where(s => s.HolidayName.Contains(searchkey) || s.name.Contains(searchkey)); }
            if (is_active == false) { src = src.Where(s => s.IsActive == true); }
            if (onlythisyear == true) { src = src.Where(s => s.HolidayDate >= first_day_of_month && s.HolidayDate <= last_day_of_month ); }

            src = src.OrderByDescending(s => s.HolidayID);
            
            return src.ToList();
        }

        public static bool VerifygetHolidayByDateandCompany(DateTime date, int company_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            bool data = db.CompanyHolidays.Any(s => s.HolidayDate == date && s.CompanyID == company_id && s.IsActive == true);
            return data;
        }


        public static CompanyHoliday getHolidayByDateandCompany(DateTime date, int company_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            
            CompanyHoliday data = db.CompanyHolidays.Where(s => s.HolidayDate == date && s.CompanyID == company_id && s.IsActive == true).OrderByDescending(s => s.HolidayID).FirstOrDefault();
            return data;
        }

        public static int getHolidayDaysCount(DateTime date, int company_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            int data = db.CompanyHolidays.Count(s => s.HolidayDate == date && s.CompanyID == company_id && s.IsActive == true);
            return data;
        }

        public static CompanyHoliday getCompanyHolidayByID(int id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            CompanyHoliday data = db.CompanyHolidays.Single(s => s.HolidayID == id);
            return data;
        }

        public static CompanyHoliday insert(CompanyHoliday data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            
            db.CompanyHolidays.Add(data);
            db.SaveChanges();

            return data;
        }

        public static CompanyHoliday update(CompanyHoliday data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            CompanyHoliday record = db.CompanyHolidays.Single(s => s.HolidayID == data.HolidayID);
            record.HolidayName = data.HolidayName;
            record.HolidayType = data.HolidayType;
            record.HolidayDate = data.HolidayDate;
            record.CompanyID = data.CompanyID;
            record.IsActive = data.IsActive;
            record.last_edit_by = data.last_edit_by;
            record.last_edit_date = data.last_edit_date;

            db.SaveChanges();

            return data;
        }

    }
}
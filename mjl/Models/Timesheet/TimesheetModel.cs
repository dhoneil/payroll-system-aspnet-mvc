using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models.Database;

namespace mjl.Models
{
    public class TimesheetModel
    {
        
        public static bool verifyHasEmployeeTimesheet(int emp_id, DateTime date_from, DateTime date_to) {
            dbPayrollEntities db = new dbPayrollEntities();
            bool flag = db.employee_timesheet.Any(s => s.employee_id == emp_id && s.date_from == date_from && s.date_to == date_to && s.status == "Approved");
            return flag;
        }

        public static employee_timesheet getEmployeeTimsheet(int emp_id, DateTime date_from, DateTime date_to) {
            dbPayrollEntities db = new dbPayrollEntities();
            employee_timesheet data = db.employee_timesheet.Where(s => s.employee_id == emp_id && s.date_from == date_from && s.date_to == date_to && s.status == "Approved").FirstOrDefault();
            return data;
        }

        public static employee_timesheet insert(employee_timesheet data) 
        {
            dbPayrollEntities db = new dbPayrollEntities();
            db.employee_timesheet.Add(data);
            db.SaveChanges();
            return data;
        }

        public static employee_timesheet cancelTimesheet(int timesheet_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            employee_timesheet record = db.employee_timesheet.Single(s => s.emp_timesheet_id == timesheet_id);
            record.status = "CANCELLED";
            record.cancelled_by = Convert.ToInt32(sysSession.UserID.ToString());
            record.cancelled_date = DateTime.Now;
            db.SaveChanges();
            return record;
        }

        public static employee_timesheet viewTimesheetById(int timesheet_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            employee_timesheet record = db.employee_timesheet.Single(s => s.emp_timesheet_id == timesheet_id);
            return record;
        }

    }
}
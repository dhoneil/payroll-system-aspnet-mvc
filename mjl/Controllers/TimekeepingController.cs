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
    public class TimekeepingController : Controller
    {
        // GET: Timekeeping
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GenerateTimesheet()
        {
            List<Company> companylst = FilterModel.getCompany();
            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ SELECT ]]"
            });
            ViewBag.company = new SelectList(companylst.Select(s=> new Company { company_id = s.company_id, name = s.name + " ( " + s.type + " ) "}).OrderBy(s => s.company_id), "company_id", "name");
            return View();
        }

        public ActionResult viewTimesheet(int company_id, string date_range)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);

            List<Employee> lstEmployee = FilterModel.getEmployee(company_id);
            ViewBag.lstEmployee = lstEmployee;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;

            return PartialView();

        }

        public ActionResult refreshTimesheet(string date_range, int employee_id)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);
            ViewBag.employee_id = employee_id;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            return PartialView();
        }

        public ActionResult viewEmployeeDtr(string date_range, int employee_id)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);
            ViewBag.employee_id = employee_id;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            return PartialView();
        }

        public ActionResult viewEmployeeDtrTwelveHoursFormat(string date_range, int employee_id)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);
            ViewBag.employee_id = employee_id;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            return PartialView();
        }

        public ActionResult insertEmployeeTimesheet(List<employee_timesheet> data, string date_range)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);

            foreach (var items in data)
            {
                if(TimesheetModel.verifyHasEmployeeTimesheet(items.employee_id.Value, date_from, date_to) == false)
                {
                    items.prepared_by = Convert.ToInt32(sysSession.UserID);
                    items.prepared_date = DateTime.Now;
                    items.date_from = date_from;
                    items.date_to = date_to;
                    items.status = "Approved";

                    TimesheetModel.insert(items);
                }
             
            }
            
            return Json(true);
        }
        public ActionResult removeDTREntry(int dtr_id)
        {
            DtrComputation.RemoveDTREntry(dtr_id);
            return Json(true);
        }

        public ActionResult cancelTimesheet(int timesheet_id)
        {
            TimesheetModel.cancelTimesheet(timesheet_id);
            return Json(true);
        }

        public ActionResult verifyTimesheetHasPayslip(int timesheet_id)
        {
            employee_timesheet timesheet = TimesheetModel.viewTimesheetById(timesheet_id);
            bool flag = PayslipModel.verifyHasPayslip(timesheet.employee_id.Value, timesheet.date_from.Value, timesheet.date_to.Value);
            return Json(flag);
        }


        public ActionResult insertDailyDtrEmployee(List<DailyTimeRecord> data)
        {
            foreach (var items in data)
            {
                DailyTimeRecord record = new DailyTimeRecord();
                Employee emp_data = EmployeeModel.getEmployeeByID(items.EmployeeID);
                record.RecordID = items.RecordID;
                record.EmployeeID = items.EmployeeID;
                record.RecordDate = items.RecordDate;
                record.TimeLogIn = items.TimeLogIn;
                record.TimeLogOut = items.TimeLogOut;
                record.SystemEqDate = 0;
                record.TimeLogType = 0;
                record.HasComputedPayslip = false;

                decimal holiday_hrs = 0;
                int lates = 0;
                int undertime = 0;
                decimal overtime = 0;
                decimal night_diff = 0;
                bool is_half_day = false;

                Employee_Schedule employee_sched = Scheduler.getEmployeeSchedule(items.EmployeeID, items.RecordDate);
                DateTime sched_DateTimeIn = record.RecordDate + TimeSpan.Parse(employee_sched.TimeIn.Value.ToString());
                DateTime sched_DateTimeOut = new DateTime();
                if (employee_sched.isOutOverDay == true)
                {
                    sched_DateTimeOut = record.RecordDate.Date.AddDays(1) + TimeSpan.Parse(employee_sched.TimeOut.Value.ToString());
                }
                else
                {
                    sched_DateTimeOut = record.RecordDate + TimeSpan.Parse(employee_sched.TimeOut.Value.ToString());
                }

                if (items.TimeLogIn > sched_DateTimeIn)
                {
                    lates = (int)Math.Round(Convert.ToDecimal(items.TimeLogIn.Value.Subtract(sched_DateTimeIn).TotalMinutes));
                }

                if (items.TimeLogOut < sched_DateTimeOut)
                {
                    undertime = (int)Math.Round(Convert.ToDecimal(sched_DateTimeOut.Subtract(items.TimeLogOut.Value).TotalMinutes));
                }

                if (items.TimeLogOut < sched_DateTimeIn.AddHours(4))
                {
                    undertime = (int)Math.Round(Convert.ToDecimal(sched_DateTimeIn.AddHours(4).Subtract(items.TimeLogOut.Value).TotalMinutes));
                    is_half_day = true;
                }

                if (items.TimeLogOut > sched_DateTimeOut)
                {
                    decimal result = Math.Round(Convert.ToDecimal(items.TimeLogOut.Value.Subtract(sched_DateTimeOut).TotalMinutes / 60), 2);
                    overtime = result;
                }

                if (HolidayModel.VerifygetHolidayByDateandCompany(items.RecordDate, emp_data.company_id.Value))
                {
                    holiday_hrs = Math.Round(Convert.ToDecimal(items.TimeLogOut.Value.Subtract(items.TimeLogIn.Value).TotalMinutes / 60), 2);

                    if (holiday_hrs > 8)
                    {
                        holiday_hrs = 8;
                    }
                }
                DateTime NightDiffStart = (Convert.ToDateTime(items.RecordDate) + TimeSpan.Parse("22:00:00"));
                DateTime NightDiffEnd = (Convert.ToDateTime(items.RecordDate) + TimeSpan.Parse("06:00:00"));
                NightDiffEnd = NightDiffEnd.AddDays(1);
                if (items.TimeLogOut >= NightDiffStart && (items.TimeLogOut <= NightDiffEnd))
                {
                    // a should be less than b

                    DateTime a = new DateTime();
                    a = NightDiffStart;
                    DateTime b = new DateTime();
                    b = items.TimeLogOut.Value;
                    night_diff = Math.Round(Convert.ToDecimal(b.Subtract(a).TotalMinutes) / 60, 2);
                    //night_diff = night_diff - (night_diff % 0.5m);
                }
                else if (items.TimeLogOut >= NightDiffStart && items.TimeLogOut > NightDiffEnd)
                {
                    night_diff = 8;
                }
                else
                {
                    night_diff = 0;
                }

                if (items.TimeLogOut > sched_DateTimeOut)
                {
                    record.total_duty_hrs = Convert.ToDecimal(sched_DateTimeOut.Subtract(items.TimeLogIn.Value).TotalMinutes) / 60;
                }
                else
                {
                    record.total_duty_hrs = Convert.ToDecimal(items.TimeLogOut.Value.Subtract(items.TimeLogIn.Value).TotalMinutes) / 60;
                }


                if (items.TimeLogOut > sched_DateTimeOut)
                {
                    record.total_duty_hrs = Math.Round(Convert.ToDecimal(sched_DateTimeOut.Subtract(items.TimeLogIn.Value).TotalMinutes) / 60);
                }
                else {
                    record.total_duty_hrs = Math.Round(Convert.ToDecimal(items.TimeLogOut.Value.Subtract(items.TimeLogIn.Value).TotalMinutes) / 60);
                }

                record.total_overtime_hrs = overtime;
                record.lates = lates;
                record.undertime = undertime;
                record.isHalfDay = is_half_day;
                record.nigthdiff = night_diff;
                record.holiday_hrs = holiday_hrs;

                if (items.RecordID > 0)
                {
                    //update
                    record.LastEditBy = Convert.ToInt32(sysSession.UserID);
                    record.LastEditDate = DateTime.Now;
                    DtrComputation.updateDailyTimeRecord(record);
                }
                else {
                    //save
                    record.PreparedBy = Convert.ToInt32(sysSession.UserID);
                    record.PreparedDate = DateTime.Now;
                    record.LastEditBy = Convert.ToInt32(sysSession.UserID);
                    record.LastEditDate = DateTime.Now;

                    DtrComputation.insertDailyTimeRecord(record);
                }

            }
            return Json(true);
        }
    }
}
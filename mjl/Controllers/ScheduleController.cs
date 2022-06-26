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
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Scheduler()
        {
            List<Company> companylst = FilterModel.getCompany();
            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ ALL ]]"
            });

            List<Job> joblst = FilterModel.getJob();
            joblst.Add(new Job
            {
                JobID = 0,
                JobName = "[[ ALL ]]"
            });

            ViewBag.company = new SelectList(companylst.Select(s => new Company { company_id = s.company_id, name = s.name + " ( " + s.type + " ) " }).OrderBy(s => s.company_id), "company_id", "name");
            ViewBag.job = new SelectList(joblst.OrderBy(s => s.JobID), "JobID", "JobName");

            return View();
        }
        public ActionResult getScheduleView(int employee_id, DateTime date)
        {
            //DateTime first_day_of_month = new DateTime(date.Year, date.Month, 1);
            //DateTime last_day_of_month = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

            DateTime first_day_of_month = new DateTime(date.Year, date.Month, 1);
            DateTime last_day_of_month = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

            ViewBag.date_from = first_day_of_month;
            ViewBag.date_to = last_day_of_month;
            ViewBag.prevMonth = first_day_of_month.AddDays(-1);

            ViewBag.employee_id = employee_id;

            return PartialView();
        }

        public ActionResult setRestday(int employee_id, DateTime date, DateTime Timein, DateTime Timeout)
        {
            Employee_Schedule emp_sched_list = new Employee_Schedule();
            emp_sched_list.EmployeeID = employee_id;
            emp_sched_list.EffectiveDate = date.Date;
            emp_sched_list.ExpiryDate = date.Date;
            emp_sched_list.TimeIn = Timein.TimeOfDay;
            emp_sched_list.TimeOut = Timeout.TimeOfDay;
            emp_sched_list.Remarks = "Schedule System Added";
            emp_sched_list.isOutOverDay = false;
            emp_sched_list.Restday = (int)date.DayOfWeek;
            emp_sched_list.prepared_by = Convert.ToInt32(sysSession.UserID.ToString());
            emp_sched_list.prepared_date = DateTime.Now;
            SchedulerModel.insertEmployeeSchedule(emp_sched_list);


            if (DtrComputation.verifyDTRComputationSingle(employee_id, date))
            {
                DtrComputation.removeDTRComputationSingle(employee_id, date);
            }
            return Json(true);
        }

        public ActionResult saveDailyScheulde(int employee_id, DateTime date, DateTime Timein, DateTime Timeout)
        {
            Employee_Schedule emp_sched_list = new Employee_Schedule();
            emp_sched_list.EmployeeID = employee_id;
            emp_sched_list.EffectiveDate = date.Date;
            emp_sched_list.ExpiryDate = date.Date;
            emp_sched_list.TimeIn = Timein.TimeOfDay;
            emp_sched_list.TimeOut = Timeout.TimeOfDay;
            emp_sched_list.Remarks = "Schedule System Added";
            emp_sched_list.prepared_by = Convert.ToInt32(sysSession.UserID.ToString());
            emp_sched_list.prepared_date = DateTime.Now;

            if (Timeout.TimeOfDay < Timein.TimeOfDay)
            {
                emp_sched_list.isOutOverDay = true;
            }
            else
            {
                emp_sched_list.isOutOverDay = false;
            }
            SchedulerModel.insertEmployeeSchedule(emp_sched_list);

            if (DtrComputation.verifyDTRComputationSingle(employee_id, date))
            {
                DtrComputation.removeDTRComputationSingle(employee_id, date);
            }
            return Json(true);
        }


        public ActionResult saveScheduleByCompanyJob(int company_id, int job_id, int employee_id, DateTime effectivity_date, DateTime expiry_date, DateTime Timein, DateTime Timeout, int restday)
        {
            if (employee_id > 0)
            {
                Employee_Schedule emp_sched_list = new Employee_Schedule();
                emp_sched_list.EmployeeID = employee_id;
                emp_sched_list.EffectiveDate = effectivity_date.Date;
                emp_sched_list.ExpiryDate = expiry_date.Date;
                emp_sched_list.TimeIn = Timein.TimeOfDay;
                emp_sched_list.TimeOut = Timeout.TimeOfDay;
                emp_sched_list.Restday = restday;
                emp_sched_list.Remarks = "Schedule System Added";
                emp_sched_list.prepared_by = Convert.ToInt32(sysSession.UserID.ToString());
                emp_sched_list.prepared_date = DateTime.Now;

                if (Timeout.TimeOfDay < Timein.TimeOfDay)
                {
                    emp_sched_list.isOutOverDay = true;
                }
                else
                {
                    emp_sched_list.isOutOverDay = false;
                }
                SchedulerModel.insertEmployeeSchedule(emp_sched_list);

                if (DtrComputation.verifyDTRComputationRange(employee_id, effectivity_date, expiry_date) == true)
                {
                    DtrComputation.removeDTRComputationRange(employee_id, effectivity_date, expiry_date);
                }
            }
            else 
            {
                List<Employee> data = FilterModel.getEmployee(company_id, job_id);
                foreach (var items in data)
                {
                    Employee_Schedule emp_sched_list = new Employee_Schedule();
                    emp_sched_list.EmployeeID = items.EmployeeID;
                    emp_sched_list.EffectiveDate = effectivity_date.Date;
                    emp_sched_list.ExpiryDate = expiry_date.Date;
                    emp_sched_list.TimeIn = Timein.TimeOfDay;
                    emp_sched_list.TimeOut = Timeout.TimeOfDay;
                    emp_sched_list.Restday = restday;
                    emp_sched_list.Remarks = "Schedule System Added";
                    emp_sched_list.prepared_by = Convert.ToInt32(sysSession.UserID.ToString());
                    emp_sched_list.prepared_date = DateTime.Now;


                    if (Timeout.TimeOfDay < Timein.TimeOfDay)
                    {
                        emp_sched_list.isOutOverDay = true;
                    }
                    else 
                    {
                        emp_sched_list.isOutOverDay = false;
                    }
                    SchedulerModel.insertEmployeeSchedule(emp_sched_list);

                    if (DtrComputation.verifyDTRComputationRange(items.EmployeeID, effectivity_date, expiry_date) == true)
                    {
                        DtrComputation.removeDTRComputationRange(items.EmployeeID, effectivity_date, expiry_date);
                    }
                }
            }
          
            return Json(true);
        }
    }
}
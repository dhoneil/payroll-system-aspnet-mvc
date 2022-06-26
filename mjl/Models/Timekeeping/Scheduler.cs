using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models.Database;

namespace mjl.Models
{
    public class Scheduler
    {

        //DHONEIL ANGCHANGCO
        public static string verifyScheduleAvailable(int employee_id, DateTime day)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            string remarks = "";

            bool data = db.Employee_Schedule.Any(s => s.EffectiveDate.Value <= day.Date && s.ExpiryDate.Value >= day.Date && s.EmployeeID == employee_id);

            if (data)
            {
                Employee_Schedule employee_sched = db.Employee_Schedule.Where(s => s.EffectiveDate.Value <= day.Date && s.ExpiryDate.Value >= day.Date && s.EmployeeID == employee_id).OrderByDescending(x => x.ScheduleID).FirstOrDefault();
                if (employee_sched.Restday == (int)day.DayOfWeek)
                {
                    remarks = "Restday";
                }
                else
                {
                    remarks = "HasSchedule";
                }
            }
            else
            {
                remarks = "NoSchedule";
            }

            return remarks;
        }

        public static Employee_Schedule getEmployeeSchedule(int employee_id, DateTime day)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Schedule employee_sched = db.Employee_Schedule.Where(s => s.EffectiveDate.Value <= day && s.ExpiryDate.Value >= day && s.EmployeeID == employee_id).OrderByDescending(x => x.ScheduleID).FirstOrDefault();
            return employee_sched;
        }







        //LEONEIL DELA CERNA
        //public static string verifyScheduleAvailable(int employee_id, DateTime day)
        //{
        //    dbPayrollEntities db = new dbPayrollEntities();
        //    string remarks = "";
        //    string query = String.Format(@"select * from Employee_Schedule where '{0}' between EffectiveDate and ExpiryDate and EmployeeID = {1} order by ScheduleID desc", day.ToShortDateString(), employee_id);

        //    bool data = db.Database.SqlQuery<Employee_Schedule>(query).Any();
        //    if (data)
        //    {
        //        Employee_Schedule employee_sched = db.Database.SqlQuery<Employee_Schedule>(query).FirstOrDefault();
        //        if (employee_sched.Restday == (int)day.DayOfWeek)
        //        {
        //            remarks = "Restday";
        //        }
        //        else
        //        {
        //            remarks = "HasSchedule";
        //        }
        //    }
        //    else
        //    {
        //        remarks = "NoSchedule";
        //    }
        //    return remarks;
        //}

        //public static Employee_Schedule getEmployeeSchedule(int employee_id, DateTime day)
        //{
        //    dbPayrollEntities db = new dbPayrollEntities();
        //    string query = String.Format(@"select * from employee_schedule where '{0}' between EffectiveDate and ExpiryDate and EmployeeID = {1} order by ScheduleID desc", day.ToShortDateString(), employee_id);
        //    Employee_Schedule employee_sched = db.Database.SqlQuery<Employee_Schedule>(query).FirstOrDefault();
        //    return employee_sched;
        //}
    }
}
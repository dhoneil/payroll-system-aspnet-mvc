using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models.Database;

namespace mjl.Models    
{
    public class SchedulerModel
    {
        public static Employee_Schedule insertEmployeeSchedule(Employee_Schedule data)
        {

            dbPayrollEntities db = new dbPayrollEntities();
            db.Employee_Schedule.Add(data);
            db.SaveChanges();
            return data;
        }

    }
}
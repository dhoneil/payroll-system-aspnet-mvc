using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mjl.Models.Database;

namespace mjl.Models
{
    public class CompensationModel
    {
        public static decimal getEmployeeCompensationMonthly(int employee_id) {

            decimal rate = 0;
            string compensation_type = "";
            dbPayrollEntities db = new dbPayrollEntities();
            if (db.CompensationHistories.Any(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true))
            {
                CompensationHistory data = db.CompensationHistories.Where(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true).Take(1).FirstOrDefault();
                compensation_type = !String.IsNullOrEmpty(data.CompensationType) ? data.CompensationType.ToString().ToUpper() : "Invalid";

                if (compensation_type == "MONTHLY")
                {
                    rate = data.CompensationRate.Value;
                }
                else if (compensation_type == "DAILY")
                {
                    //DAILY
                    rate = (data.CompensationRate.Value * 313) / 12;
                }
                else {
                    rate = 1;
                }
            }
            return rate;
        }

        public static string getCompensationType(int employee_id)
        {

            string compensation_type = "";
            dbPayrollEntities db = new dbPayrollEntities();
            if (db.CompensationHistories.Any(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true))
            {
                CompensationHistory data = db.CompensationHistories.Where(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true).Take(1).FirstOrDefault();
                compensation_type = data.CompensationType.ToString().ToUpper();
            }
            return compensation_type;
            
        }

        public static decimal getCompensationRate(int employee_id)
        {
            decimal rate = 0;
            string compensation_type = "";
            dbPayrollEntities db = new dbPayrollEntities();
            if (db.CompensationHistories.Any(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true))
            {
                CompensationHistory data = db.CompensationHistories.Where(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true).Take(1).FirstOrDefault();
                compensation_type = data.CompensationType.ToString().ToUpper();

                if (compensation_type == "MONTHLY")
                {
                    rate = data.CompensationRate.Value;
                }
                else
                {
                    //DAILY
                    rate = data.CompensationRate.Value;
                }
            }
            return rate;

        }

        public static decimal getCompensationRateWorkingDays(int employee_id, int total_wd)
        {
            decimal rate = 0;
            string compensation_type = "";
            dbPayrollEntities db = new dbPayrollEntities();
            if (db.CompensationHistories.Any(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true))
            {
                CompensationHistory data = db.CompensationHistories.Where(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true).Take(1).FirstOrDefault();
                compensation_type = data.CompensationType.ToString().ToUpper();

                if (compensation_type == "MONTHLY")
                {
                    rate = data.CompensationRate.Value;
                }
                else {
                    //DAILY
                    rate = total_wd * data.CompensationRate.Value;
                }
            }
            return rate;

        }

        public static decimal getHourlyRate(int employee_id)
        {
            decimal rate = 0;
            decimal hourly = 0;
           
            string compensation_type = "";
            dbPayrollEntities db = new dbPayrollEntities();
            if (db.CompensationHistories.Any(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true))
            {
                CompensationHistory data = db.CompensationHistories.Where(s => s.ValidityStart <= DateTime.Now && s.EmployeeID == employee_id && s.IsActive == true).Take(1).FirstOrDefault();
                compensation_type = data.CompensationType.ToString().ToUpper();

                if (compensation_type == "MONTHLY")
                {
                    rate = data.CompensationRate.Value;
                    hourly = ((rate * 12) / 392.5m) / 8;
                    
                }
                else
                {
                    //DAILY
                    rate = data.CompensationRate.Value;
                    hourly = rate / 8;
                }
            }
            return hourly;

        }
        


    }

    public enum EnumCompensationType
    {
        HOURLY,
        WEEKLY,
        MONTHLY,
        PROJECT_BASED,
        SEMI_MONTHLY,
        DAILY
    };
}
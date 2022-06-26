using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models.Database;

namespace mjl.Models
{
    public class DtrComputation
    {

        public static List<DailyTimeRecord> getDailyTimeRecord(int employee_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<DailyTimeRecord> record = db.DailyTimeRecords.Where(s => s.EmployeeID == employee_id && s.RecordDate >= date_from && s.RecordDate <= date_to).ToList();
            return record;
        }

        public static Boolean verifyDTRComputationRange(int employee_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            bool data = db.DailyTimeRecords.Any(s => s.EmployeeID == employee_id && s.RecordDate >= date_from && s.RecordDate <= date_to);
            return data;
        }

        public static Boolean verifyDTRComputationSingle(int employee_id, DateTime date)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            bool data = db.DailyTimeRecords.Any(s => s.EmployeeID == employee_id && s.RecordDate == date);
            return data;
        }
        
        public static void removeDTRComputationRange(int employee_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<DailyTimeRecord> data = db.DailyTimeRecords.Where(s => s.EmployeeID == employee_id && s.RecordDate >= date_from && s.RecordDate <= date_to).ToList();
            db.DailyTimeRecords.RemoveRange(data);
            db.SaveChanges();
        }

        public static void removeDTRComputationSingle(int employee_id,DateTime date)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<DailyTimeRecord> data = db.DailyTimeRecords.Where(s => s.EmployeeID == employee_id && s.RecordDate == date).ToList();
            db.DailyTimeRecords.RemoveRange(data);
            db.SaveChanges();
        }

        public static DailyTimeRecord insertDailyTimeRecord(DailyTimeRecord data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            db.DailyTimeRecords.Add(data);
            db.SaveChanges();
            return data;
        }

        public static DailyTimeRecord updateDailyTimeRecord(DailyTimeRecord data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            DailyTimeRecord record = db.DailyTimeRecords.Single(s => s.RecordID == data.RecordID);
            record.EmployeeID = data.EmployeeID;
            record.RecordDate = data.RecordDate;
            record.TimeLogIn = data.TimeLogIn;
            record.TimeLogOut = data.TimeLogOut;
            record.LastEditBy = data.LastEditBy;
            record.LastEditDate = data.LastEditDate;
            record.total_duty_hrs = data.total_duty_hrs;
            record.total_overtime_hrs = data.total_overtime_hrs.Value;
            record.nigthdiff = data.nigthdiff.Value;
            record.lates = data.lates;
            record.undertime = data.undertime;
            record.isHalfDay = data.isHalfDay;
            record.holiday_hrs = data.holiday_hrs;

            db.SaveChanges();

            return data;
        }

        public static List<DailyTimeRecord> getEmployeeDtrCovered(DateTime date_from, DateTime date_to, int employee_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<DailyTimeRecord> data = db.DailyTimeRecords.Where(s => s.RecordDate >= date_from && s.RecordDate <= date_to && s.EmployeeID == employee_id).ToList();
            return data;
        }

        public static DailyTimeRecord getDtrReport(int employee_id, DateTime date)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            DailyTimeRecord data = new DailyTimeRecord();

            data = db.DailyTimeRecords.Single(s => s.EmployeeID == employee_id && s.RecordDate == date);
            
            return data;
        }

        public static bool checkgetDtrReport(int employee_id, DateTime date)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            bool flag = false;

            flag = db.DailyTimeRecords.Any(s => s.EmployeeID == employee_id && s.RecordDate == date);

            return flag;
        }

        public static void RemoveDTREntry(int dtr_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            DailyTimeRecord data = db.DailyTimeRecords.Single(s => s.RecordID == dtr_id);
            db.DailyTimeRecords.Remove(data);
            db.SaveChanges();

        }


        

    }
    public class computation_dtr_details
    {
        public decimal working_days { get; set; }
        public decimal holidays { get; set; }
        public decimal days_absent { get; set; }
        public decimal duty_hours { get; set; }
        public decimal lates { get; set; }
        public decimal undertime { get; set; }
        public decimal overtime { get; set; }

    }
}
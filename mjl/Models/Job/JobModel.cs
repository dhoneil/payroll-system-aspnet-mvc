using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models;
using mjl.Models.Database;

namespace mjl.Models
{
    public class JobModel
    {
        public static Job getJobPerEmployeID(int job_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Job record = db.Jobs.Single(s => s.JobID == job_id);
            return record;
        }

        public static List<Job> GetAllData()
        {
            dbPayrollEntities db = new dbPayrollEntities();

            IQueryable<Job> src = db.Jobs;

            return src.ToList();
        }

        public static Job insert(Job data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            
            db.Jobs.Add(data);

            db.SaveChanges();

            return data;
        }

        public static List<Job> GetData(string job_name, bool show_inactive)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            IQueryable<Job> src = db.Jobs.Where(s => s.JobName.Contains(job_name));
            if (show_inactive == false) { src = src.Where(s => s.IsActive == true); }

            src = src.OrderByDescending(s => s.JobID);

            return src.ToList();
        }

        public static Job update(Job data)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            Job src = db.Jobs.Single(s=>s.JobID == data.JobID);

            src.JobName = data.JobName;
            src.JobDescription = data.JobDescription;
            src.LastEditBy = data.LastEditBy;
            src.LastEditDate = data.LastEditDate;
            src.IsActive = data.IsActive;

            db.SaveChanges();

            return src;
        }
    }
}
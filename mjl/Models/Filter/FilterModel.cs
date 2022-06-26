using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models.Database;

namespace mjl.Models.Filter
{
    public class FilterModel
    {

        public static List<Employee> getEmployee(int company_id=0, int job_id=0, int employee_id = 0) {

            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<Employee> qry = db.Employees.Where(s => s.IsActive == true);
            if (company_id > 0)
                qry = qry.Where(s => s.company_id == company_id);
            if (job_id > 0)
                qry = qry.Where(s => s.JobID == job_id);
            if (employee_id > 0)
                qry = qry.Where(s => s.EmployeeID == employee_id);

            return qry.OrderBy(s=>s.LastName).ToList();
        }

        public static List<Company> getCompany(int company_id = 0) {

            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<Company> qry = db.Companies.Where(s => s.is_active == true);

            if (company_id > 0)
                qry = db.Companies.Where(s => s.company_id == company_id);
            return qry.ToList();
        }

        public static List<Job> getJob(int job_id = 0)
        {

            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<Job> qry = db.Jobs.Where(s => s.IsActive == true);
            if (job_id > 0)
                qry = db.Jobs.Where(s => s.JobID == job_id);

            return qry.ToList();
        }
    }
}
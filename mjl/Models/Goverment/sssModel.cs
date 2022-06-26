using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mjl.Models.Database;

namespace mjl.Models
{
    public class sssModel
    {
        public static decimal getSSSAmount(decimal monthly_rate)
        {
            decimal value = 0;
            dbPayrollEntities db = new dbPayrollEntities();
            string query = String.Format(@"select * from SSS_Contribution where {0} between start_range and end_range",monthly_rate);
            bool verify = db.Database.SqlQuery<sss_model>(query).Any();
            decimal total_contribution = 0;
            if (verify == false)
            {
                total_contribution = 0;
            }
            else {
                sss_model data = db.Database.SqlQuery<sss_model>(query).Single();
                total_contribution = data.total_contribution_ee;
            }
            return total_contribution;
        }
        public static List<SSS_Contribution> getSSSContribution()
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<SSS_Contribution> details = db.SSS_Contribution.ToList();
            return details;
        }
    }
    public class sss_model
    {
        public int id { get; set; }
        public decimal total_contribution_ee { get; set; }

    }
}
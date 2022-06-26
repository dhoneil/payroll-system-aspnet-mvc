using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using mjl.Models.Database;

namespace mjl.Models
{
    public class ChargeModel
    {

        public static List<viewEmployeeCharge> GetData(string searchkey, bool show_inactive)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<viewEmployeeCharge> src = db.viewEmployeeCharges;

            if (!String.IsNullOrEmpty(searchkey)) { src = src.Where(s => s.ChargeName.Contains(searchkey) || s.FirstName.Contains(searchkey) || s.LastName.Contains(searchkey)); }
            
            if (show_inactive == false) { src = src.Where(s => s.status != "CANCELLED"); }
            src = src.OrderByDescending(s => s.ChargeID);
            return src.ToList();
        }

        public static List<viewChargeReport> getChargeByCompany(int company_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<viewChargeReport> src = db.viewChargeReports.Where(s=> s.status == "APPROVED");


            src = src.Where(s => s.date_from >= date_from && s.date_to <= date_to);
            if (company_id > 0) { src = src.Where(s => s.company_id == company_id); }


            return src.OrderBy(s => s.ChargeName).ThenBy(s=> s.LastName).ToList();
        }


        public static viewEmployeeCharge getChargeDataById(int charge_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            viewEmployeeCharge data = db.viewEmployeeCharges.Single(s => s.ChargeID == charge_id);
            return data;
        }

        public static EmployeeCharge insert(EmployeeCharge data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            db.EmployeeCharges.Add(data);
            db.SaveChanges();

            return data;
        }

        public static EmployeeCharge releaseCharge(int charge_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            EmployeeCharge src = db.EmployeeCharges.Single(s => s.ChargeID == charge_id);
            src.status = "APPROVED";
            src.Released_by = Convert.ToInt32(sysSession.UserID);
            src.Released_date = DateTime.Now;

            db.SaveChanges();
            return src;
        }
        public static EmployeeCharge cancelCharge(int charge_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            EmployeeCharge src = db.EmployeeCharges.Single(s => s.ChargeID == charge_id);
            src.status = "CANCELLED";
            src.Cancelled_by = Convert.ToInt32(sysSession.UserID);
            src.Cancelled_date = DateTime.Now;

            db.SaveChanges();
            return src;
        }

        public static EmployeeCharge update(EmployeeCharge data)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            EmployeeCharge src = db.EmployeeCharges.Single(s => s.ChargeID == data.ChargeID);

            src.ChargeName = data.ChargeName;
            src.ChargeAmount = data.ChargeAmount;
            src.date_from = data.date_from;
            src.date_to = data.date_to;
            src.lastedit_by = data.lastedit_by;
            src.lastedit_date = data.lastedit_date;

            db.SaveChanges();

            return src;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using mjl.Models;
using mjl.Models.Database;

namespace mjl.Models
{
    public class EmployeeChargeModel
    {
        public static List<EmployeeCharge> getEmployeeCharge(int employee_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<EmployeeCharge> list_employee_charge = db.EmployeeCharges.Where(s => s.EmployeeID == employee_id && s.date_from == date_from && s.date_to == date_to && s.status == "APPROVED").ToList();
            return list_employee_charge;
        }
        public static void updateEmployeeChargeReference(int payslip_id,int payslip_adjusment_id, int charge_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            EmployeeCharge record = db.EmployeeCharges.Single(s => s.ChargeID == charge_id);
            record.payslip_adjustment_id = payslip_adjusment_id;
            record.payslip_id = payslip_id;
            db.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models.Database;

namespace mjl.Models
{
    public class PayslipModel
    {

        //EMPLOYEE PAYSLIP Function
        #region
        public static Employee_Payslip insertPayslip(Employee_Payslip data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip record = db.Employee_Payslip.Add(data);
            db.SaveChanges();
            return record;
        }

        public static bool verifyHasPayslip(int emp_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            bool flag = db.Employee_Payslip.Any(s => s.employee_id == emp_id && s.date_from == date_from && s.date_to == date_to && s.status != "CANCELLED");
            return flag;
        }

        public static bool verifyHasPayslipApproved(int emp_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            bool flag = db.Employee_Payslip.Any(s => s.employee_id == emp_id && s.date_from == date_from && s.date_to == date_to && s.status == "APPROVED");
            return flag;
        }


        public static Employee_Payslip updatePayslip(Employee_Payslip data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip record = db.Employee_Payslip.Single(s => s.payslip_id == data.payslip_id);
            record.total_additionals = data.total_additionals.Value;
            record.total_less = data.total_less.Value;
            record.total_deduction = data.total_deduction.Value;
            record.gross_pay = data.gross_pay;
            record.net_pay = data.net_pay;

            db.SaveChanges();
            return record;
        }

        public static Employee_Payslip getPayslip(int emp_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip record = db.Employee_Payslip.Single(s => s.employee_id == emp_id && s.date_from == date_from && s.date_to == date_to && s.status != "CANCELLED");
            return record;
        }

        public static List<Employee_Payslip> getPayslipByCompany(int company_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<Employee_Payslip> record = db.Employee_Payslip.Where(s => s.company_id == company_id && s.date_from == date_from && s.date_to == date_to && s.status != "CANCELLED").ToList();
            return record;
        }

        public static Employee_Payslip getPayslipByID(int payslip_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip record = db.Employee_Payslip.Single(s => s.payslip_id == payslip_id);
            return record;
        }

        public static Employee_Payslip PayslipChangeStatus(int payslip_id, string status)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip record = db.Employee_Payslip.Single(s => s.payslip_id == payslip_id);
            if (status == "APPROVED")
            {
                record.status = "APPROVED";
                record.approved_by = Convert.ToInt32(sysSession.UserID.ToString());
                record.approved_date = DateTime.Now;
            }
            else if (status == "CANCELLED")
            {

                record.status = "CANCELLED";
                record.cancelled_by = Convert.ToInt32(sysSession.UserID.ToString());
                record.cancelled_date = DateTime.Now;
            }
            
            db.SaveChanges();

            return record;
        }

        #endregion
        //END

        //PAYSLIP Details Function
        #region
        public static Employee_Payslip_Details insertPayslipDetails(Employee_Payslip_Details data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip_Details record = db.Employee_Payslip_Details.Add(data);
            db.SaveChanges();
            return record;
        }
        public static List<Employee_Payslip_Details> getPayslipDetails(string type="", int payslip_id = 0)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<Employee_Payslip_Details> record = db.Employee_Payslip_Details.Where(s => s.payslip_id == payslip_id && s.type == type).ToList();
            return record;
        }

        public static List<Employee_Payslip_Details> getPayslipDetailsByName(string name = "", int payslip_id = 0)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<Employee_Payslip_Details> record = db.Employee_Payslip_Details.Where(s => s.payslip_id == payslip_id && s.name == name).ToList();
            return record;
        }

        public static Employee_Payslip_Details updateEmployeePayslipDetails(Employee_Payslip_Details data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip_Details record = db.Employee_Payslip_Details.Single(s => s.payslip_less_id == data.payslip_less_id);
            record.amount = data.amount.Value;
            record.last_edit_by = data.last_edit_by;
            record.last_edit_date = data.last_edit_date;
            db.SaveChanges();
            return record;
            
        }

        public static Employee_Payslip_Government updateEmployeePayslipGovernment(Employee_Payslip_Government data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip_Government record = db.Employee_Payslip_Government.Single(s => s.payslip_government_id == data.payslip_government_id);
            record.amount = data.amount.Value;
            record.last_edit_by = data.last_edit_by;
            record.last_edit_date = data.last_edit_date;
            db.SaveChanges();
            return record;
        }
        #endregion
        //END


        //Payslip Government Function
        #region
        public static Employee_Payslip_Government insertPayslipGovernment(Employee_Payslip_Government data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip_Government record = db.Employee_Payslip_Government.Add(data);
            db.SaveChanges();
            return record;
        }
        public static List<Employee_Payslip_Government> getPayslipGovernment(int payslip_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<Employee_Payslip_Government> record = db.Employee_Payslip_Government.Where(s => s.payslip_id == payslip_id).ToList();
            return record;
        }
        #endregion
        //END 


        //Payslip Adjustment Function
        #region
        public static Employee_Payslip_Adjustment insertPayslipAdjustment(Employee_Payslip_Adjustment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip_Adjustment record = db.Employee_Payslip_Adjustment.Add(data);
            db.SaveChanges();
            return record;

        }
        public static List<Employee_Payslip_Adjustment> getPayslipAdjustmentCharge(int payslip_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<Employee_Payslip_Adjustment> record = db.Employee_Payslip_Adjustment.Where(s => s.payslip_id == payslip_id && s.isCharges == true && s.status != "CANCELLED").ToList();
            return record;

        }
        public static List<Employee_Payslip_Adjustment> getPayslipAdjustment(int payslip_id, string type = "")
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<Employee_Payslip_Adjustment> record = db.Employee_Payslip_Adjustment.Where(s => s.payslip_id == payslip_id && s.isCharges == false && s.type == type && s.status != "CANCELLED").ToList();
            return record;

        }
        public static Employee_Payslip_Adjustment updatPayslipAmount(Employee_Payslip_Adjustment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip_Adjustment record = db.Employee_Payslip_Adjustment.Single(s => s.payslip_adjustment_id == data.payslip_adjustment_id);
            record.amount = data.amount.Value;
            record.lastedit_by = data.lastedit_by;
            record.lastedit_date = data.lastedit_date;
            db.SaveChanges();
            return record;
        }
        public static Employee_Payslip_Adjustment cancelPayslipAdjustment(Employee_Payslip_Adjustment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee_Payslip_Adjustment record = db.Employee_Payslip_Adjustment.Single(s => s.payslip_adjustment_id == data.payslip_adjustment_id);
            record.status = "CANCELLED";
            record.cancelled_by = data.cancelled_by;
            record.cancelled_date = data.cancelled_date;
            db.SaveChanges();
            return record;
        }
        #endregion
        //END

    }
}
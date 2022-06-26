using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using mjl.Models.Database;

namespace mjl.Models
{
    public class BillingSecurityModel
    {
        public static int getNextIDBilling()
        {
            dbPayrollEntities db = new dbPayrollEntities();
            return db.BillingSecurities.Count() + 1;
        }

        public static BillingSecurity insert(BillingSecurity data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            db.BillingSecurities.Add(data);
            db.SaveChanges();

            return data;
        }
        public static BillingSecurity update(BillingSecurity data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            BillingSecurity record = db.BillingSecurities.Single(s => s.bill_id == data.bill_id);
            record.bill_description = data.bill_description;
            record.bill_amount = data.bill_amount;
            record.updated_by = Convert.ToInt32(sysSession.UserID);
            record.update_date = DateTime.Now;
            db.SaveChanges();

            return data;
        }

        public static BillingSecurity getBillingSecurity(int bill_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            BillingSecurity record = db.BillingSecurities.Single(s => s.bill_id == bill_id);
            return record;
        }

        public static BillingSecurity getBilling(int company_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            BillingSecurity record = db.BillingSecurities.Single(s => s.company_id == company_id && s.date_from == date_from && s.date_to == date_to);
            return record;
        }

        public static bool verifyBilling(int company_id, DateTime date_from, DateTime date_to)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            bool flag = db.BillingSecurities.Any(s => s.company_id == company_id && s.date_from == date_from && s.date_to == date_to);
            return flag;
        }

        public static BillingAdjustment insertbillingadjustment(BillingAdjustment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            db.BillingAdjustments.Add(data);
            db.SaveChanges();

            return data;
        }

        public static decimal getSumByPayslip(int payslip_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            decimal result = Convert.ToDecimal(db.BillingAdjustments.Where(s => s.payslip_id == payslip_id &&s.is_active==true).Sum(s => s.amount));
            return result;
        }

        public static List<View_billingAdjustments> getAdjustmentsByPayslipId(int payslip_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<View_billingAdjustments> query = db.View_billingAdjustments;
            if (payslip_id>0)
            {
                query = query.Where(s => s.payslip_id == payslip_id && s.is_active==true);
            }
            return query.OrderByDescending(s => s.prepared_date).ThenBy(s => s.name).ToList();
        }
        public static BillingAdjustment updateBillingAdjustment(BillingAdjustment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            BillingAdjustment qwe = db.BillingAdjustments.Single(s => s.id == data.id);

            qwe.id = data.id;
            qwe.name = data.name;
            qwe.amount = data.amount;
            qwe.last_edit_by = data.last_edit_by; 
            qwe.last_edit_date = data.last_edit_date;
            db.SaveChanges();

            return data;
        }
        public static BillingAdjustment removeBillingAdjustment(BillingAdjustment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            BillingAdjustment qwe = db.BillingAdjustments.Single(s => s.id == data.id);
            qwe.id = data.id;
            qwe.is_active = data.is_active;
            db.SaveChanges();

            return data;
        }


    }
}
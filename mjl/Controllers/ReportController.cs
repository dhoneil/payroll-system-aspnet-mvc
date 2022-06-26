using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using mjl.Models.Database;
using mjl.Models.Filter;
using mjl.Models;


namespace mjl.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewChargeReport()
        {

            return View();
        }
      
        public ActionResult GeneratePayslipBilling()
        {
            List<Company> companylst = FilterModel.getCompany();
            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ SELECT ]]"
            });
            ViewBag.company = new SelectList(companylst.Select(s => new Company { company_id = s.company_id, name = s.name + " ( " + s.type + " ) " }).OrderBy(s => s.company_id), "company_id", "name");
            return View();
        }
        public ActionResult ViewPayslipBilling(int company_id, string date_range)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);
            
            List<Employee_Payslip> listEmpPayslip = PayslipModel.getPayslipByCompany(company_id, date_from, date_to);
            Company company = CompanyModel.getCompany(company_id);


            ViewBag.company = company;
            ViewBag.listEmpPayslip = listEmpPayslip;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;



            return PartialView();
        }

        public ActionResult ViesSecurityBilling(int company_id, string date_range)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);

            List<Employee_Payslip> listEmpPayslip = PayslipModel.getPayslipByCompany(company_id, date_from, date_to);
            Company company = CompanyModel.getCompany(company_id);

            if (BillingSecurityModel.verifyBilling(company_id, date_from, date_to))
            {
                ViewBag.hasBilling = true;
                ViewBag.billing = BillingSecurityModel.getBilling(company_id, date_from, date_to);
            }
            else {
                ViewBag.hasBilling = false;
            }

            ViewBag.company = company;
            ViewBag.listEmpPayslip = listEmpPayslip;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;

            return PartialView();
        }

        public ActionResult checkCompanyStatus(int company_id)
        {
            Company company = CompanyModel.getCompany(company_id);

            return Json(company.type.ToUpper());
        }

        public ActionResult insertBilling(BillingSecurity data, string date_range)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);

            data.date_from = date_from;
            data.date_to = date_to;
            data.prepared_by = Convert.ToInt32(sysSession.UserID.ToString());
            data.prepared_date = DateTime.Now;

            BillingSecurityModel.insert(data);

            return Json(true);
        }
        public ActionResult updateBilling(BillingSecurity data)
        {

            BillingSecurityModel.update(data);

            return Json(true);
        }
        public ActionResult printBillingSecurity(int bill_id)
        {
            BillingSecurity billing = BillingSecurityModel.getBillingSecurity(bill_id);
            Company company = CompanyModel.getCompany(billing.company_id.Value);


            ViewBag.company = company;
            ViewBag.billing = billing;
            return PartialView();
        }

        public ActionResult PayslipReport()
        {
            List<Company> companylst = FilterModel.getCompany();
            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ SELECT ]]"
            });
            ViewBag.company = new SelectList(companylst.Select(s => new Company { company_id = s.company_id, name = s.name + " ( " + s.type + " ) " }).OrderBy(s => s.company_id), "company_id", "name");
            return View();
        }

        public ActionResult _PayslipReport(int company_id, string date_range)
        {

            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);
            Company company = CompanyModel.getCompany(company_id);

            List<Employee> lstEmployee = FilterModel.getEmployee(company_id);
            ViewBag.lstEmployee = lstEmployee;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            ViewBag.company = company.name.ToUpper();

            return PartialView();

        }

        public ActionResult ChargeReport()
        {
            List<Company> companylst = FilterModel.getCompany();
            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ SELECT ]]"
            });
            ViewBag.company = new SelectList(companylst.Select(s => new Company { company_id = s.company_id, name = s.name + " ( " + s.type + " ) " }).OrderBy(s => s.company_id), "company_id", "name");
            return View();
        }

        public ActionResult _ChargeResult(int company_id, string date_range)
        {

            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);
            Company company = CompanyModel.getCompany(company_id);

            List<viewChargeReport> charge_report = ChargeModel.getChargeByCompany(company_id, date_from, date_to);
            ViewBag.charge_report = charge_report;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;
            ViewBag.company = company.name.ToUpper();

            return PartialView();

        }


        public ActionResult save_adjustmentBilling(int payslip_id, string adjustment_name, decimal adjustment_amount)
        {
            BillingAdjustment src = new BillingAdjustment();
            src.payslip_id = payslip_id;
            src.name = adjustment_name;
            src.amount = adjustment_amount;
            src.is_active = true;
            src.prepared_by = Convert.ToInt32(sysSession.UserID);
            src.prepared_date = DateTime.Now;
            BillingSecurityModel.insertbillingadjustment(src);
            return Json(true);
        }

        public ActionResult _getBillingadjustmentsbyPayslipId(int payslip_id)
        {
            List<View_billingAdjustments> src = BillingSecurityModel.getAdjustmentsByPayslipId(payslip_id);
            ViewBag.billingadjustments = src;
            return PartialView();
        }

        public ActionResult updateAdjustment(int adjustment_id, string adjustment_name, decimal adjustment_amount)
        {
            BillingAdjustment src = new BillingAdjustment();
            src.id = adjustment_id;
            src.name = adjustment_name;
            src.amount = adjustment_amount;
            src.last_edit_by = Convert.ToInt32(sysSession.UserID);
            src.last_edit_date = DateTime.Now;
            BillingSecurityModel.updateBillingAdjustment(src);
            return Json(true);
        }

        public ActionResult remove_adjustmentBilling(int adjustment_id)
        {
            BillingAdjustment src = new BillingAdjustment();
            src.id = adjustment_id;
            src.is_active = false;
            BillingSecurityModel.removeBillingAdjustment(src);
            return Json(true);
        }
    }
}
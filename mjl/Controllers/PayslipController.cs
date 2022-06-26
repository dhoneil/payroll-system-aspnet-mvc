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
    public class PayslipController : Controller
    {
        // GET: Payslip
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneratePayslip()
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

        public ActionResult ViewPayslip(int company_id, string date_range)
        {
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);

            List<Employee> lstEmployee = FilterModel.getEmployee(company_id);
            ViewBag.lstEmployee = lstEmployee;
            ViewBag.date_from = date_from;
            ViewBag.date_to = date_to;

            return PartialView();
        }
        
        public ActionResult ViewPayslipPrint(int payslip_id)
        {

            Employee_Payslip emp_payslip = PayslipModel.getPayslipByID(payslip_id);
            employee_timesheet emp_timesheet = TimesheetModel.getEmployeeTimsheet(emp_payslip.employee_id.Value, emp_payslip.date_from.Value, emp_payslip.date_to.Value);
            List<Employee_Payslip_Details> payslip_details_less = PayslipModel.getPayslipDetails("LESS", payslip_id);
            List<Employee_Payslip_Details> payslip_details_add = PayslipModel.getPayslipDetails("ADD", payslip_id);
            List<Employee_Payslip_Government> payslip_government = PayslipModel.getPayslipGovernment(payslip_id);

            List<Employee_Payslip_Adjustment> payslip_adjustment_charge = PayslipModel.getPayslipAdjustmentCharge(payslip_id);
            List<Employee_Payslip_Adjustment> payslip_adjustment_deduction = PayslipModel.getPayslipAdjustment(payslip_id, "DEDUCTION");
            List<Employee_Payslip_Adjustment> payslip_adjustment_additional = PayslipModel.getPayslipAdjustment(payslip_id, "ADDITIONAL");


            ViewBag.emp_payslip = emp_payslip;
            ViewBag.emp_timesheet = emp_timesheet;
            ViewBag.payslip_details_less = payslip_details_less;
            ViewBag.payslip_details_add = payslip_details_add;
            ViewBag.payslip_government = payslip_government;
            ViewBag.payslip_adjustment_charge = payslip_adjustment_charge;
            ViewBag.payslip_adjustment_deduction = payslip_adjustment_deduction;
            ViewBag.payslip_adjustment_additional = payslip_adjustment_additional;

            return PartialView();
        }

        public ActionResult viewPayslipDetails(int payslip_id)
        {
            Employee_Payslip emp_payslip = PayslipModel.getPayslipByID(payslip_id);
            List<Employee_Payslip_Details> payslip_details_less = PayslipModel.getPayslipDetails("LESS", payslip_id);
            List<Employee_Payslip_Details> payslip_details_add = PayslipModel.getPayslipDetails("ADD", payslip_id);
            List<Employee_Payslip_Government> payslip_government = PayslipModel.getPayslipGovernment(payslip_id);

            List<Employee_Payslip_Adjustment> payslip_adjustment_charge = PayslipModel.getPayslipAdjustmentCharge(payslip_id);
            List<Employee_Payslip_Adjustment> payslip_adjustment_deduction = PayslipModel.getPayslipAdjustment(payslip_id, "DEDUCTION");
            List<Employee_Payslip_Adjustment> payslip_adjustment_additional = PayslipModel.getPayslipAdjustment(payslip_id, "ADDITIONAL");


            ViewBag.emp_payslip = emp_payslip;
            ViewBag.payslip_details_less = payslip_details_less;
            ViewBag.payslip_details_add = payslip_details_add;
            ViewBag.payslip_government = payslip_government;
            ViewBag.payslip_adjustment_charge = payslip_adjustment_charge;
            ViewBag.payslip_adjustment_deduction = payslip_adjustment_deduction;
            ViewBag.payslip_adjustment_additional = payslip_adjustment_additional;

            return PartialView();
        }

        public ActionResult changePayslipStatus(int payslip_id, string status)
        {
            PayslipModel.PayslipChangeStatus(payslip_id, status);
            return Json(true);
        }

        public ActionResult viewPayslipLogs(int payslip_id)
        {
            ViewBag.payslip = PayslipModel.getPayslipByID(payslip_id);
            return PartialView();
        }

        public ActionResult insertEntryDeduction(int payslip_id, string deduction_name, decimal deduction_amount)
        {

           
            Employee_Payslip_Adjustment employee_payslip_adjustment = new Employee_Payslip_Adjustment();
            employee_payslip_adjustment.payslip_id = payslip_id;
            employee_payslip_adjustment.name = deduction_name;
            employee_payslip_adjustment.amount = deduction_amount;
            employee_payslip_adjustment.type = "DEDUCTION";
            employee_payslip_adjustment.isCharges = false;
            employee_payslip_adjustment.prepared_by = Convert.ToInt32(sysSession.UserID);
            employee_payslip_adjustment.prepared_date = DateTime.Now;
            employee_payslip_adjustment.status = "APPROVED";

            PayslipModel.insertPayslipAdjustment(employee_payslip_adjustment);
            
            return Json(true);
            
        }
        
        public ActionResult insertEntryAdditional(int payslip_id, string adjustment_name, decimal adjustment_amount)
        {
            
            Employee_Payslip_Adjustment employee_payslip_adjustment = new Employee_Payslip_Adjustment();
            employee_payslip_adjustment.payslip_id = payslip_id;
            employee_payslip_adjustment.name = adjustment_name;
            employee_payslip_adjustment.amount = adjustment_amount;
            employee_payslip_adjustment.type = "ADDITIONAL";
            employee_payslip_adjustment.isCharges = false;
            employee_payslip_adjustment.prepared_by = Convert.ToInt32(sysSession.UserID);
            employee_payslip_adjustment.prepared_date = DateTime.Now;
            employee_payslip_adjustment.status = "APPROVED";

            PayslipModel.insertPayslipAdjustment(employee_payslip_adjustment);

            return Json(true);


        }
        public ActionResult updatePayslipLessDetails(Employee_Payslip_Details data)
        {
            data.last_edit_by = Convert.ToInt32(sysSession.UserID);
            data.last_edit_date = DateTime.Now;
            PayslipModel.updateEmployeePayslipDetails(data);
            return Json(true);
        }

        public ActionResult updatePayslipGovernment(Employee_Payslip_Government data)
        {
            data.last_edit_by = Convert.ToInt32(sysSession.UserID);
            data.last_edit_date = DateTime.Now;
            PayslipModel.updateEmployeePayslipGovernment(data);
            return Json(true);
        }
        public ActionResult updatePayslipAdjustment(Employee_Payslip_Adjustment data)
        {
            data.lastedit_by = Convert.ToInt32(sysSession.UserID);
            data.lastedit_date = DateTime.Now;
            PayslipModel.updatPayslipAmount(data);
            return Json(true);
        }

        public ActionResult cancelPayslipAdjusment(Employee_Payslip_Adjustment data)
        {
            data.cancelled_by = Convert.ToInt32(sysSession.UserID);
            data.cancelled_date = DateTime.Now;
            PayslipModel.cancelPayslipAdjustment(data);
            return Json(true);
        }

        public ActionResult refreshPayslip(int payslip_id)
        {
            Employee_Payslip payslip = PayslipModel.getPayslipByID(payslip_id);

            employee_timesheet emp_timesheet = TimesheetModel.getEmployeeTimsheet(payslip.employee_id.Value, payslip.date_from.Value, payslip.date_to.Value);
            decimal salary_rate = CompensationModel.getCompensationRateWorkingDays(payslip.employee_id.Value, emp_timesheet.regular_days.Value);

            decimal total_less = PayslipModel.getPayslipDetails("LESS", payslip_id).Sum(s => s.amount.Value);
            decimal total_additional = PayslipModel.getPayslipDetails("ADD", payslip_id).Sum(s => s.amount.Value);

            decimal total_adjustment = PayslipModel.getPayslipAdjustment(payslip_id, "ADDITIONAL").Sum(s => s.amount.Value);

            decimal total_deduction = PayslipModel.getPayslipAdjustment(payslip_id, "DEDUCTION").Sum(s => s.amount.Value);
            decimal total_government = PayslipModel.getPayslipGovernment(payslip_id).Sum(s => s.amount.Value);
            decimal total_charge = PayslipModel.getPayslipAdjustmentCharge(payslip_id).Sum(s => s.amount.Value);

            decimal over_all_deduction = total_deduction + total_government + total_charge;



            decimal total_gross = (salary_rate + total_additional) - total_less;
            decimal total_netpay = (total_gross - over_all_deduction) + total_adjustment;

            Employee_Payslip updateEmployeePayslip = new Employee_Payslip();
            updateEmployeePayslip.payslip_id = payslip.payslip_id;
            updateEmployeePayslip.total_additionals = total_additional;
            updateEmployeePayslip.total_less = total_less;
            updateEmployeePayslip.total_deduction = over_all_deduction;
            updateEmployeePayslip.gross_pay = total_gross;
            updateEmployeePayslip.net_pay = total_netpay;

            PayslipModel.updatePayslip(updateEmployeePayslip);

            Employee_Payslip emp_payslip = PayslipModel.getPayslipByID(payslip_id);
            ViewBag.emp_payslip = emp_payslip;

            return PartialView();
        }

        public ActionResult ComputePayslip(int employee_id, string date_range)
        {
            
            string[] date_range_split = date_range.Split('-');
            DateTime date_from = Convert.ToDateTime(date_range_split[0]);
            DateTime date_to = Convert.ToDateTime(date_range_split[1]);

            employee_timesheet emp_timesheet = TimesheetModel.getEmployeeTimsheet(employee_id, date_from, date_to);
            Employee emp_data = EmployeeModel.getEmployeeByID(employee_id);
            Company company = CompanyModel.getCompany(emp_data.company_id.Value);
            string compensation_type = CompensationModel.getCompensationType(employee_id);
            decimal salary_rate = CompensationModel.getCompensationRateWorkingDays(employee_id, emp_timesheet.regular_days.Value);
            decimal compensation_rate = CompensationModel.getCompensationRate(employee_id);

            decimal rate_by_hour = CompensationModel.getHourlyRate(employee_id);

            List<DailyTimeRecord> employee_dtr = DtrComputation.getDailyTimeRecord(employee_id, date_from, date_to);

            Employee_Payslip emp_payslip = new Employee_Payslip();
            emp_payslip.company_id = emp_data.company_id.Value;
            emp_payslip.employee_id = employee_id;
            emp_payslip.employee_rate = compensation_rate;
            emp_payslip.date_from = date_from;
            emp_payslip.date_to = date_to;
            emp_payslip.total_additionals = 0;
            emp_payslip.total_less = 0;
            emp_payslip.total_deduction = 0;
            emp_payslip.gross_pay = 0;
            emp_payslip.net_pay = 0;
            emp_payslip.status = "FOR APPROVAL";
            emp_payslip.prepared_by = Convert.ToInt32(sysSession.UserID);
            emp_payslip.prepared_date = DateTime.Now;

            Employee_Payslip payslip = PayslipModel.insertPayslip(emp_payslip);

            

            decimal lates = 0;
            decimal lates_amount = 0;

            decimal undertime = 0;
            decimal undertime_amount = 0;
            

            decimal total_overtime_amount = 0;

      
            decimal absent_amount = 0;
            decimal absent_days = 0;
            decimal night_diff_amount = 0;
            decimal night_hrs = 0;



           
            decimal ordinary_ot_amount = 0;
            decimal ordinary_ot_min = 0;

            decimal rest_day_hrs = 0;
            decimal rest_day_hrs_amount = 0;
            decimal rest_day_ot_amount = 0;
            decimal rest_day_ot_min = 0;

            decimal regular_hld_hrs = 0;
            decimal regular_hld_hrs_amount = 0;
            decimal regular_hld_ot_amount = 0;
            decimal regular_hld_ot_min = 0;

            decimal regular_restday_hld_hrs = 0;
            decimal regular_restday_hld_hrs_amount = 0;
            decimal regular_restday_hld_ot_amount = 0;
            decimal regular_restday_hld_ot_min = 0;

            decimal special_hld_hrs = 0;
            decimal special_hld_hrs_amount = 0;
            decimal special_hld_ot_amount = 0;
            decimal special_hld_ot_min = 0;

            decimal special_restday_hld_hrs = 0;
            decimal special_restday_hld_hrs_amount = 0;
            decimal special_restday_hld_ot_amount = 0;
            decimal special_restday_hld_ot_min = 0;

            decimal double_hld_hrs = 0;
            decimal double_hld_hrs_amount = 0;
            decimal double_hld_hrs_ot_amount = 0;
            decimal double_hld_hrs_ot_min = 0;


            foreach (var daily_employee_dtr in employee_dtr)
            {
                string remarks_schedule = Scheduler.verifyScheduleAvailable(daily_employee_dtr.EmployeeID, daily_employee_dtr.RecordDate);
                
                //TYPE LESS
                if (daily_employee_dtr.lates > 0)
                {
                    //Calculate Lates
                    #region
                    if (HolidayModel.VerifygetHolidayByDateandCompany(daily_employee_dtr.RecordDate, emp_data.company_id.Value) || remarks_schedule == "Restday")
                    {
                        //Holiday and Restday duty no lates and undertime
                    }
                    else {
                        lates += daily_employee_dtr.lates.Value;
                        lates_amount += ComputationModel.getLatesComputation(daily_employee_dtr.lates.Value, employee_id);
                    }
                 
                    #endregion 

                }
                if (daily_employee_dtr.undertime > 0)
                {
                    //Calculate Undertime
                    #region
                    if (HolidayModel.VerifygetHolidayByDateandCompany(daily_employee_dtr.RecordDate, emp_data.company_id.Value) || remarks_schedule == "Restday")
                    {
                        //Holiday and Restday duty no lates and undertime
                    }
                    else {
                        undertime += daily_employee_dtr.undertime.Value;
                        undertime_amount += ComputationModel.getUndertimeComputataion(daily_employee_dtr.undertime.Value, employee_id);
                    }
                    #endregion
                }
                //TYPE ADD
                if (daily_employee_dtr.total_overtime_hrs > 0)
                {
                    //Calculate Overtime
                    #region
                    decimal overtime_amount = 0;
                    //if (HolidayModel.VerifygetHolidayByDateandCompany(daily_employee_dtr.RecordDate, employee_id))
                    //{
                    //    //holiday_overtime_hrs += daily_employee_dtr.total_overtime_hrs.Value;
                    //    //holiday_overtime_amount += ComputationModel.getOvertimeComputation(daily_employee_dtr.total_overtime_hrs.Value, daily_employee_dtr.RecordDate, emp_data.company_id.Value, emp_data.EmployeeID);

                    //}
                    //else {
                    //    regular_overtime_hrs += daily_employee_dtr.total_overtime_hrs.Value;
                    //    regular_overtime_amount += ComputationModel.getOvertimeComputation(daily_employee_dtr.total_overtime_hrs.Value, daily_employee_dtr.RecordDate, emp_data.company_id.Value, emp_data.EmployeeID);
                    //}
                    overtime_amount = ComputationModel.getOvertimeComputation(daily_employee_dtr.total_overtime_hrs.Value, daily_employee_dtr.RecordDate, emp_data.company_id.Value, emp_data.EmployeeID);
                    string overt_time_belong = ComputationModel.getType(daily_employee_dtr.RecordDate, emp_data.company_id.Value, emp_data.EmployeeID);

                    if (overt_time_belong == "Double Holiday")
                    {
                        double_hld_hrs_ot_min += daily_employee_dtr.total_overtime_hrs.Value;
                        double_hld_hrs_ot_amount += overtime_amount;
                       
                    }
                    else if (overt_time_belong == "Regular Restday")
                    {
                        regular_restday_hld_ot_min += daily_employee_dtr.total_overtime_hrs.Value;
                        regular_restday_hld_ot_amount += overtime_amount;
                    }
                    else if (overt_time_belong == "Special Restday")
                    {
                        special_restday_hld_ot_min += daily_employee_dtr.total_overtime_hrs.Value;
                        special_restday_hld_ot_amount += overtime_amount;
                    }
                    else if (overt_time_belong == "Restday")
                    {
                        rest_day_ot_min += daily_employee_dtr.total_overtime_hrs.Value;
                        rest_day_ot_amount += overtime_amount;
                    }
                    else if (overt_time_belong == "Regular")
                    {
                        regular_hld_ot_min += daily_employee_dtr.total_overtime_hrs.Value;
                        regular_hld_ot_amount += overtime_amount;
                    }
                    else if (overt_time_belong == "Special")
                    {
                        special_hld_ot_min += daily_employee_dtr.total_overtime_hrs.Value;
                        special_hld_ot_amount += overtime_amount;
                    }
                    else
                    {
                        //Ordinary
                        ordinary_ot_min += daily_employee_dtr.total_overtime_hrs.Value;
                        ordinary_ot_amount += overtime_amount;
                    }
                    #endregion

                }
                if(daily_employee_dtr.nigthdiff > 0)
                {
                    //Calculate Nigtdiff
                    #region
                    night_hrs += daily_employee_dtr.nigthdiff.Value;
                    night_diff_amount += ComputationModel.getNightDiffComputation(daily_employee_dtr.nigthdiff.Value, daily_employee_dtr.RecordDate, emp_data.company_id.Value, emp_data.EmployeeID);
                    #endregion
                }

                //Work in Restday and Holiday
                if (HolidayModel.VerifygetHolidayByDateandCompany(daily_employee_dtr.RecordDate, emp_data.company_id.Value) || remarks_schedule == "Restday")
                {
                    //Calculate first 8hrs holiday
                    #region
                    decimal hld_amount = 0;
                    //Manpower == DAILY
                    decimal regular_hrs = daily_employee_dtr.total_duty_hrs.Value;
                    if (regular_hrs > 8)
                    {
                        regular_hrs = 8;
                    }
                    if (company.type.EmptyNull() == "MANPOWER")
                    {
                        hld_amount = ComputationModel.getHolidayHrs(daily_employee_dtr.holiday_hrs.Value, regular_hrs, daily_employee_dtr.RecordDate, emp_data.company_id.Value, emp_data.EmployeeID);
                    }
                    else {
                        decimal daily_rate = rate_by_hour * 8;
                        hld_amount = ComputationModel.getHolidayHrs(daily_employee_dtr.holiday_hrs.Value, regular_hrs, daily_employee_dtr.RecordDate, emp_data.company_id.Value, emp_data.EmployeeID) - daily_rate;
                    }

                    string holiday_type = ComputationModel.getType(daily_employee_dtr.RecordDate, emp_data.company_id.Value, emp_data.EmployeeID);

                    if (holiday_type == "Double Holiday")
                    {
                        double_hld_hrs += daily_employee_dtr.holiday_hrs.Value;
                        double_hld_hrs_amount += hld_amount;
                    }
                    else if (holiday_type == "Regular Restday")
                    {
                        regular_restday_hld_hrs += daily_employee_dtr.holiday_hrs.Value;
                        regular_restday_hld_hrs_amount += hld_amount;
                    }
                    else if (holiday_type == "Special Restday")
                    {
                        special_restday_hld_hrs += daily_employee_dtr.holiday_hrs.Value;
                        special_restday_hld_hrs_amount += hld_amount;
                    }
                    else if (holiday_type == "Restday")
                    {
                        rest_day_hrs += regular_hrs;
                        rest_day_hrs_amount += hld_amount;
                    }
                    else if (holiday_type == "Regular")
                    {
                        regular_hld_hrs += daily_employee_dtr.holiday_hrs.Value;
                        regular_hld_hrs_amount += hld_amount;
                    }
                    else if (holiday_type == "Special")
                    {
                        special_hld_hrs += daily_employee_dtr.holiday_hrs.Value;
                        special_hld_hrs_amount += hld_amount;
                    }
                    else
                    {
                        //Ordinary DTR
                        
                    }
                    #endregion

                }

            }


            if (emp_timesheet.absent_days > 0)
            {
                // Calculate Absent
                #region
                decimal daily_rate = rate_by_hour * 8;
                absent_amount = daily_rate * emp_timesheet.absent_days.Value;
                absent_days = emp_timesheet.absent_days.Value;
                #endregion

            }
            
           

            EmployeeGovernment emp_government = EmployeeGovernmentModel.getEmployeeGovermentDetails(employee_id);
            decimal employee_monthly_rate_for_government = CompensationModel.getEmployeeCompensationMonthly(employee_id);

            decimal sss_amount = 0;
            decimal philhealth_amount = 0;
            decimal pagibig_amount = 0;

            if (emp_government.with_sss == true)
            {
                //GET SSS Deduction Amount
                #region
                if (emp_government.sss_type_deduction == "MANUAL")
                {
                    sss_amount = emp_government.sss_amount.Value;
                }
                else {
                    
                    sss_amount = sssModel.getSSSAmount(employee_monthly_rate_for_government);
                }
                #endregion'

            }
            if (emp_government.with_philhealth == true)
            {
                //GET SSS PHILHEALTH Amount
                #region
                if (emp_government.philhealth_type_deduction == "MANUAL")
                {
                    philhealth_amount = emp_government.philhealth_amount.Value; 
                }
                else {
                    philhealth_amount = philhealthModel.getphilhealthAmount(employee_monthly_rate_for_government);
                }
                #endregion

            }
            if (emp_government.with_pagibig == true)
            {
                //GET SSS PAGIBIG Amount
                #region
                if (emp_government.pagibig_type_deduction == "MANUAL")
                {
                    pagibig_amount = emp_government.pagibig_amount.Value;
                }
                else {
                    pagibig_amount = 100;
                }
                #endregion

            }

            //Employe Charges
            #region
            List<EmployeeCharge> employeeCharges = EmployeeChargeModel.getEmployeeCharge(employee_id, date_from, date_to);
            decimal total_employee_adjustment_deduction = 0;
            foreach (var items in employeeCharges)
            {
                total_employee_adjustment_deduction += items.ChargeAmount.Value;
                Employee_Payslip_Adjustment employee_payslip_adjustment = new Employee_Payslip_Adjustment();
                employee_payslip_adjustment.payslip_id = payslip.payslip_id;
                employee_payslip_adjustment.name = items.ChargeName.ToUpper();
                employee_payslip_adjustment.amount = items.ChargeAmount.Value;
                employee_payslip_adjustment.type = "DEDUCTION";
                employee_payslip_adjustment.isCharges = true;
                employee_payslip_adjustment.prepared_by = Convert.ToInt32(sysSession.UserID);
                employee_payslip_adjustment.prepared_date = DateTime.Now;
                employee_payslip_adjustment.status = "APPROVED";

                Employee_Payslip_Adjustment record = PayslipModel.insertPayslipAdjustment(employee_payslip_adjustment);
                EmployeeChargeModel.updateEmployeeChargeReference(record.payslip_id.Value, record.payslip_adjustment_id, items.ChargeID);
                
            }
            #endregion

            //Save Government Deductions
            #region

            Employee_Payslip_Government emp_payslip_government_deduction_sss = new Employee_Payslip_Government();
            emp_payslip_government_deduction_sss.payslip_id = payslip.payslip_id;
            emp_payslip_government_deduction_sss.name = "SSS";
            emp_payslip_government_deduction_sss.amount = sss_amount;

            PayslipModel.insertPayslipGovernment(emp_payslip_government_deduction_sss);


            Employee_Payslip_Government emp_payslip_government_deduction_philheath = new Employee_Payslip_Government();
            emp_payslip_government_deduction_philheath.payslip_id = payslip.payslip_id;
            emp_payslip_government_deduction_philheath.name = "PHILHEALTH";
            emp_payslip_government_deduction_philheath.amount = philhealth_amount;

            PayslipModel.insertPayslipGovernment(emp_payslip_government_deduction_philheath);

            Employee_Payslip_Government emp_payslip_government_deduction_pagibig = new Employee_Payslip_Government();
            emp_payslip_government_deduction_pagibig.payslip_id = payslip.payslip_id;
            emp_payslip_government_deduction_pagibig.name = "PAGIBIG";
            emp_payslip_government_deduction_pagibig.amount = pagibig_amount;

            PayslipModel.insertPayslipGovernment(emp_payslip_government_deduction_pagibig);

            #endregion

            //Save Payslip Details 
            #region
            //Save Absences
          
            Employee_Payslip_Details emp_payslip_details_absences = new Employee_Payslip_Details();
            emp_payslip_details_absences.payslip_id = payslip.payslip_id;
            emp_payslip_details_absences.name = "Absences";
            emp_payslip_details_absences.days = absent_days;
            emp_payslip_details_absences.amount = absent_amount;
            emp_payslip_details_absences.type = "LESS";

            PayslipModel.insertPayslipDetails(emp_payslip_details_absences);

            //Save Lates
            Employee_Payslip_Details emp_payslip_details_lates = new Employee_Payslip_Details();
            emp_payslip_details_lates.payslip_id = payslip.payslip_id;
            emp_payslip_details_lates.name = "Lates";
            emp_payslip_details_lates.hours = lates;
            emp_payslip_details_lates.amount = lates_amount;
            emp_payslip_details_lates.type = "LESS";

            PayslipModel.insertPayslipDetails(emp_payslip_details_lates);

            //Save Undertime
            Employee_Payslip_Details emp_payslip_details_undertime = new Employee_Payslip_Details();
            emp_payslip_details_undertime.payslip_id = payslip.payslip_id;
            emp_payslip_details_undertime.name = "Undertime";
            emp_payslip_details_undertime.hours = undertime;
            emp_payslip_details_undertime.amount = undertime_amount;
            emp_payslip_details_undertime.type = "LESS";

            PayslipModel.insertPayslipDetails(emp_payslip_details_undertime);


            //Save NightDiff
            Employee_Payslip_Details emp_payslip_details_night_diff = new Employee_Payslip_Details();
            emp_payslip_details_night_diff.payslip_id = payslip.payslip_id;
            emp_payslip_details_night_diff.name = "Night Differential";
            emp_payslip_details_night_diff.hours = night_hrs;
            emp_payslip_details_night_diff.amount = night_diff_amount;
            emp_payslip_details_night_diff.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_night_diff);

            //Save Regular Overtime
            Employee_Payslip_Details emp_payslip_details_ordinary_ot = new Employee_Payslip_Details();
            emp_payslip_details_ordinary_ot.payslip_id = payslip.payslip_id;
            emp_payslip_details_ordinary_ot.name = "Regular Overtime";
            emp_payslip_details_ordinary_ot.hours = ordinary_ot_min;
            emp_payslip_details_ordinary_ot.amount = ordinary_ot_amount;
            emp_payslip_details_ordinary_ot.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_ordinary_ot);


            //Restday
            Employee_Payslip_Details emp_payslip_details_restday = new Employee_Payslip_Details();
            emp_payslip_details_restday.payslip_id = payslip.payslip_id;
            emp_payslip_details_restday.name = "Restday";
            emp_payslip_details_restday.hours = rest_day_hrs;
            emp_payslip_details_restday.amount = rest_day_hrs_amount;
            emp_payslip_details_restday.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_restday);

            //Restday Overtime
            Employee_Payslip_Details emp_payslip_details_ot = new Employee_Payslip_Details();
            emp_payslip_details_ot.payslip_id = payslip.payslip_id;
            emp_payslip_details_ot.name = "Restday Overtime";
            emp_payslip_details_ot.hours = rest_day_ot_min;
            emp_payslip_details_ot.amount = rest_day_ot_amount;
            emp_payslip_details_ot.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_ot);

            //Regular Holiday
            Employee_Payslip_Details emp_payslip_details_reg_hld = new Employee_Payslip_Details();
            emp_payslip_details_reg_hld.payslip_id = payslip.payslip_id;
            emp_payslip_details_reg_hld.name = "Regular Holiday";
            emp_payslip_details_reg_hld.hours = regular_hld_hrs;
            emp_payslip_details_reg_hld.amount = regular_hld_hrs_amount;
            emp_payslip_details_reg_hld.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_reg_hld);

            //Regular Holiday Overtime
            Employee_Payslip_Details emp_payslip_details_reg_hld_ot = new Employee_Payslip_Details();
            emp_payslip_details_reg_hld_ot.payslip_id = payslip.payslip_id;
            emp_payslip_details_reg_hld_ot.name = "Regular Holiday Overtime";
            emp_payslip_details_reg_hld_ot.hours = regular_hld_ot_min;
            emp_payslip_details_reg_hld_ot.amount = regular_hld_ot_amount;
            emp_payslip_details_reg_hld_ot.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_reg_hld_ot);


            //Regular Restday
            Employee_Payslip_Details emp_payslip_details_reg_rest_hld = new Employee_Payslip_Details();
            emp_payslip_details_reg_rest_hld.payslip_id = payslip.payslip_id;
            emp_payslip_details_reg_rest_hld.name = "Regular Holiday Restday";
            emp_payslip_details_reg_rest_hld.hours = regular_restday_hld_hrs;
            emp_payslip_details_reg_rest_hld.amount = regular_restday_hld_hrs_amount;
            emp_payslip_details_reg_rest_hld.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_reg_rest_hld);

            //Regular Restday Overtime
            Employee_Payslip_Details emp_payslip_details_reg_rest_hld_ot = new Employee_Payslip_Details();
            emp_payslip_details_reg_rest_hld_ot.payslip_id = payslip.payslip_id;
            emp_payslip_details_reg_rest_hld_ot.name = "Regular Holiday Restday Overtime";
            emp_payslip_details_reg_rest_hld_ot.hours = regular_restday_hld_ot_min;
            emp_payslip_details_reg_rest_hld_ot.amount = regular_restday_hld_ot_amount;
            emp_payslip_details_reg_rest_hld_ot.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_reg_rest_hld_ot);


            //Special Holiday
            Employee_Payslip_Details emp_payslip_details_spec_hld = new Employee_Payslip_Details();
            emp_payslip_details_spec_hld.payslip_id = payslip.payslip_id;
            emp_payslip_details_spec_hld.name = "Special Holiday";
            emp_payslip_details_spec_hld.hours = special_hld_hrs;
            emp_payslip_details_spec_hld.amount = special_hld_hrs_amount;
            emp_payslip_details_spec_hld.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_spec_hld);

            //Special Holiday Overtime
            Employee_Payslip_Details emp_payslip_details_spec_hld_ot = new Employee_Payslip_Details();
            emp_payslip_details_spec_hld_ot.payslip_id = payslip.payslip_id;
            emp_payslip_details_spec_hld_ot.name = "Special Holiday Overtime";
            emp_payslip_details_spec_hld_ot.hours = special_hld_ot_min;
            emp_payslip_details_spec_hld_ot.amount = special_hld_ot_amount;
            emp_payslip_details_spec_hld_ot.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_details_spec_hld_ot);


            //Special Holiday Restday
            Employee_Payslip_Details emp_payslip_detail_spec__rest_hld = new Employee_Payslip_Details();
            emp_payslip_detail_spec__rest_hld.payslip_id = payslip.payslip_id;
            emp_payslip_detail_spec__rest_hld.name = "Special Holiday Restday";
            emp_payslip_detail_spec__rest_hld.hours = special_restday_hld_hrs;
            emp_payslip_detail_spec__rest_hld.amount = special_restday_hld_hrs_amount;
            emp_payslip_detail_spec__rest_hld.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_detail_spec__rest_hld);
             
            //Special Holiday Restday Overtime
            Employee_Payslip_Details emp_payslip_detail_spec__rest_hld_ot = new Employee_Payslip_Details();
            emp_payslip_detail_spec__rest_hld_ot.payslip_id = payslip.payslip_id;
            emp_payslip_detail_spec__rest_hld_ot.name = "Special Holiday Restday Overtime";
            emp_payslip_detail_spec__rest_hld_ot.hours = special_restday_hld_ot_min;
            emp_payslip_detail_spec__rest_hld_ot.amount = special_restday_hld_ot_amount;
            emp_payslip_detail_spec__rest_hld_ot.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_detail_spec__rest_hld_ot);

            
            //Double Pay
            Employee_Payslip_Details emp_payslip_detail_double_hld = new Employee_Payslip_Details();
            emp_payslip_detail_double_hld.payslip_id = payslip.payslip_id;
            emp_payslip_detail_double_hld.name = "Double Holiday";
            emp_payslip_detail_double_hld.hours = double_hld_hrs;
            emp_payslip_detail_double_hld.amount = double_hld_hrs_amount;
            emp_payslip_detail_double_hld.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_detail_double_hld);



            //Double Pay
            Employee_Payslip_Details emp_payslip_detail_double_hld_ot = new Employee_Payslip_Details();
            emp_payslip_detail_double_hld_ot.payslip_id = payslip.payslip_id;
            emp_payslip_detail_double_hld_ot.name = "Double Holiday Overtime";
            emp_payslip_detail_double_hld_ot.hours = double_hld_hrs_ot_min;
            emp_payslip_detail_double_hld_ot.amount = double_hld_hrs_ot_amount;
            emp_payslip_detail_double_hld_ot.type = "ADD";

            PayslipModel.insertPayslipDetails(emp_payslip_detail_double_hld_ot);
            #endregion


            decimal total_additional = night_diff_amount +
                ordinary_ot_amount + 
                rest_day_hrs_amount + 
                rest_day_ot_amount +
                regular_hld_hrs_amount + 
                regular_hld_ot_amount + 
                regular_restday_hld_hrs_amount + 
                special_hld_hrs_amount + 
                special_hld_ot_amount + 
                special_restday_hld_hrs_amount + 
                special_restday_hld_ot_amount + 
                double_hld_hrs_amount +
                double_hld_hrs_ot_amount;

            decimal total_government_deduction = sss_amount + philhealth_amount + pagibig_amount;
            decimal total_adjustment_deduction = total_employee_adjustment_deduction;


            decimal total_less = lates_amount + undertime_amount + absent_amount;
         

            decimal total_deduction = total_government_deduction + total_adjustment_deduction;



            decimal total_gross = (salary_rate + total_additional) - total_less;
            decimal total_netpay = total_gross - total_deduction;


            Employee_Payslip updateEmployeePayslip = new Employee_Payslip();
            updateEmployeePayslip.payslip_id = payslip.payslip_id;
            updateEmployeePayslip.total_additionals = total_additional;
            updateEmployeePayslip.total_less = total_less;
            updateEmployeePayslip.total_deduction = total_deduction;
            updateEmployeePayslip.gross_pay = total_gross;
            updateEmployeePayslip.net_pay = total_netpay;

            PayslipModel.updatePayslip(updateEmployeePayslip);

            return Json(true);
        }
        public ActionResult GeneratePayslipBilling()
        {
            List<Company> companylst = FilterModel.getCompany();
            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ SELECT ]]"
            });
            ViewBag.company = new SelectList(companylst.OrderBy(s => s.company_id), "company_id", "Name");
            return View();
        }
    }
}
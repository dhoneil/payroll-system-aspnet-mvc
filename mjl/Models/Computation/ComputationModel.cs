using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using mjl.Models.Database;


namespace mjl.Models
{
    public class ComputationModel
    {

        public static decimal getNightDiffComputation(decimal nightdiff_hrs, DateTime date, int company_id, int employee_id)
        {
            //Calculate Night diff
            #region
            decimal nighdiffhrs = nightdiff_hrs;
            decimal nigh_diff = 0;
          
          
            string remarks_schedule = Scheduler.verifyScheduleAvailable(employee_id, date);
            decimal rate_by_hour = CompensationModel.getHourlyRate(employee_id);

            if (HolidayModel.getHolidayDaysCount(date, company_id) > 1)
            {
                if (remarks_schedule == "Restday")
                {
                    //Double Holiday and at the same time Rest day Night Shift
                    decimal nigth_diff_holiday = (rate_by_hour * 3.90m);
                    nigh_diff = (nigth_diff_holiday * 0.10m) * nighdiffhrs;
                }
                else
                {
                    //Double Holiday Night Shift
                    decimal nigth_diff_holiday = (rate_by_hour * 3.30m);
                    nigh_diff = (nigth_diff_holiday * 0.10m) * nighdiffhrs;
                }

            }
            else
            {
                if (remarks_schedule == "Restday")
                {
                    if (HolidayModel.VerifygetHolidayByDateandCompany(date, company_id))
                    {
                        //HOLIDAY and RESTDAY
                        CompanyHoliday emp_holiday = HolidayModel.getHolidayByDateandCompany(date, company_id);
                        if (emp_holiday.HolidayType == "REGULAR")
                        {
                            //Regular Holiday and at the same time Rest day Night Shift
                            decimal nigth_diff_holiday = (rate_by_hour * 2.6m);
                            nigh_diff = (nigth_diff_holiday * 0.10m) * nighdiffhrs;
                        }
                        else
                        {
                            //Special Holiday and at the same time Rest day Night Shift
                            decimal nigth_diff_holiday = (rate_by_hour * 1.5m);
                            nigh_diff = (nigth_diff_holiday * 0.10m) * nighdiffhrs;

                        }

                    }
                    else
                    {
                        //Rest day Night Shift
                        decimal nigth_diff_restday = (rate_by_hour * 1.3m);
                        nigh_diff = (nigth_diff_restday * 0.10m) * nighdiffhrs;
                    }
                }
                else
                {
                    if (HolidayModel.VerifygetHolidayByDateandCompany(date, company_id))
                    {
                        //NORMAL with Holiday
                        CompanyHoliday emp_holiday = HolidayModel.getHolidayByDateandCompany(date, company_id);
                        if (emp_holiday.HolidayType == "REGULAR")
                        {
                            //Regular Holiday Night Shift
                            decimal nigth_diff_holiday = (rate_by_hour * 2.0m);
                            nigh_diff = (nigth_diff_holiday * 0.10m) * nighdiffhrs;
                        }
                        else
                        {
                            //Special Holiday Night Shift
                            decimal nigth_diff_holiday = (rate_by_hour * 1.3m);
                            nigh_diff = (nigth_diff_holiday * 0.10m) * nighdiffhrs;

                        }
                    }
                    else
                    {
                        //Ordinary day Night Shift
                        nigh_diff = (rate_by_hour * 0.10m) * nighdiffhrs;
                    }

                }
            }
            return nigh_diff;
            #endregion
        }

        public static decimal getOvertimeComputation(decimal ot_hrs, DateTime date, int company_id, int employee_id)
        {
            //Calculate Overtime
            #region
            string remarks_schedule = Scheduler.verifyScheduleAvailable(employee_id, date);
            decimal rate_by_hour = CompensationModel.getHourlyRate(employee_id);

            decimal overtime_hrs = ot_hrs;
            decimal overtime_amount = 0;

            if (HolidayModel.getHolidayDaysCount(date, company_id) > 1)
            {
                if (remarks_schedule == "Restday")
                {
                    //On Double Holiday and at the same time Rest day Overtime
                    decimal rate_ot = (rate_by_hour * 5.07m);
                    overtime_amount = rate_ot * ot_hrs;
                }
                else
                {
                    //On Double Holiday Overtime
                    decimal rate_ot = (rate_by_hour * 3.90m);
                    overtime_amount = rate_ot * ot_hrs;
                }
            }
            else 
            {
                if (remarks_schedule == "Restday")
                {
                    if (HolidayModel.VerifygetHolidayByDateandCompany(date, company_id))
                    {
                        //HOLIDAY and RESTDAY
                        CompanyHoliday emp_holiday = HolidayModel.getHolidayByDateandCompany(date, company_id);
                        if (emp_holiday.HolidayType == "REGULAR")
                        {
                            //On Regular Holiday and at the same time Rest day Overtime
                            decimal rate_ot = (rate_by_hour * 3.38m);
                            overtime_amount = rate_ot * ot_hrs;
                        }
                        else
                        {
                            //On Special Holiday and at the same time rest day overtime
                            decimal rate_ot = (rate_by_hour * 1.95m);
                            overtime_amount = rate_ot * ot_hrs;
                        }
                    }
                    else
                    {
                        //On Rest day Overtime
                        decimal rate_ot = (rate_by_hour * 1.69m);
                        overtime_amount = rate_ot * ot_hrs;
                    }
                }
                else
                {
                    if (HolidayModel.VerifygetHolidayByDateandCompany(date, company_id))
                    {
                        //NORMAL with Holiday
                        CompanyHoliday emp_holiday = HolidayModel.getHolidayByDateandCompany(date, company_id);
                        if (emp_holiday.HolidayType == "REGULAR")
                        {
                            //On Regular Holiday Overtime
                            decimal rate_ot = (rate_by_hour * 2.60m);
                            overtime_amount = rate_ot * ot_hrs;
                        }
                        else
                        {
                            //On Special Holiday Overtime
                            decimal rate_ot = (rate_by_hour * 1.69m);
                            overtime_amount = rate_ot * ot_hrs;
                        }
                    }
                    else
                    {
                        //Ordinary day Overtime
                        decimal rate_ot = (rate_by_hour * 1.25m);
                        overtime_amount = rate_ot * ot_hrs;
                    }

                }

            }
            return overtime_amount;
            #endregion
        }


        public static string getType(DateTime date, int company_id, int employee_id)
        {
            //Calculate Holiday
            #region
            string overtime_belong = "";

            string remarks_schedule = Scheduler.verifyScheduleAvailable(employee_id, date);
            if (HolidayModel.getHolidayDaysCount(date, company_id) > 1)
            {
                if (remarks_schedule == "Restday")
                {
                    //Working on a Double Holiday and at the same time Rest day
                    overtime_belong = "Double Holiday";
                }
                else
                {
                    //Working on a Double Holiday
                    overtime_belong = "Double Holiday";
                }
            }
            else
            {
                if (remarks_schedule == "Restday")
                {
                    if (HolidayModel.VerifygetHolidayByDateandCompany(date, company_id))
                    {
                        //HOLIDAY and RESTDAY
                        CompanyHoliday emp_holiday = HolidayModel.getHolidayByDateandCompany(date, company_id);
                        if (emp_holiday.HolidayType == "REGULAR")
                        {
                            //Working on Legal/Regular Holiday and at the same time Rest day
                            overtime_belong = "Regular Restday";
                        }
                        else
                        {
                            //Working on Special Holiday and at the same time Rest day
                            overtime_belong = "Special Restday";
                        }

                    }
                    else
                    {
                        //Working on Rest day
                        overtime_belong = "Restday";
                        //decimal rate_hd = (rate_by_hour * 1.30m);
                        //holiday_amount = rate_hd * holiday_hrs;
                    }
                }
                else
                {
                    if (HolidayModel.VerifygetHolidayByDateandCompany(date, company_id))
                    {
                        //NORMAL with Holiday
                        CompanyHoliday emp_holiday = HolidayModel.getHolidayByDateandCompany(date, company_id);
                        if (emp_holiday.HolidayType == "REGULAR")
                        {
                            //Working on Legal/Regular Holiday
                            overtime_belong = "Regular";
                        }
                        else
                        {
                            //Working on Special Holiday 
                            overtime_belong = "Special";
                        }
                    }
                    else {
                        overtime_belong = "Ordinary";
                    }
                 

                }

            }
            return overtime_belong;
            #endregion
        }
        public static decimal getHolidayHrs(decimal hld_hrs, decimal rd_hrs, DateTime date, int company_id, int employee_id)
        {
            //Calculate Holiday
            #region
            string overtime_belong = "";

            string remarks_schedule = Scheduler.verifyScheduleAvailable(employee_id, date);
            decimal rate_by_hour = CompensationModel.getHourlyRate(employee_id);
            decimal daily = rate_by_hour * 8;
            decimal holiday_hrs = hld_hrs;
            decimal holiday_amount = 0;

            if (HolidayModel.getHolidayDaysCount(date, company_id) > 1)
            {
                if (remarks_schedule == "Restday")
                {
                    //Working on a Double Holiday and at the same time Rest day
                    decimal rate_hd = (rate_by_hour * 3.00m);
                    holiday_amount = rate_hd * holiday_hrs;
                }
                else
                {
                    //Working on a Double Holiday
                    decimal rate_hd = (rate_by_hour * 3.00m);
                    holiday_amount = rate_hd * holiday_hrs;
                }
            }
            else
            {
                if (remarks_schedule == "Restday")
                {
                    if (HolidayModel.VerifygetHolidayByDateandCompany(date, company_id))
                    {
                        //HOLIDAY and RESTDAY
                        CompanyHoliday emp_holiday = HolidayModel.getHolidayByDateandCompany(date, company_id);
                        if (emp_holiday.HolidayType == "REGULAR")
                        {
                            //Working on Legal/Regular Holiday and at the same time Rest day
                            decimal rate_hd = (rate_by_hour * 2.60m);
                            holiday_amount = rate_hd * holiday_hrs;
                        }
                        else
                        {
                            //Working on Special Holiday and at the same time Rest day
                            decimal rate_hd = (rate_by_hour * 1.50m);
                            holiday_amount = rate_hd * holiday_hrs;
                        }

                    }
                    else
                    {
                        //Working on Rest day
                        decimal rate_hd = (rate_by_hour * 1.30m);
                        holiday_amount = rate_hd * rd_hrs;
                    }
                }
                else
                {
                    //NORMAL with Holiday
                    CompanyHoliday emp_holiday = HolidayModel.getHolidayByDateandCompany(date, company_id);
                    if (emp_holiday.HolidayType == "REGULAR")
                    {
                        //Working on Legal/Regular Holiday
                        decimal rate_hd = (rate_by_hour * 2.00m);
                        holiday_amount = rate_hd * holiday_hrs;
                    }
                    else
                    {
                        //Working on Special Holiday 
                        decimal rate_hd = (rate_by_hour * 1.30m);
                        holiday_amount = rate_hd * holiday_hrs;
                    }

                }

            }
            return holiday_amount;
            #endregion
        }


        public static decimal getLatesComputation(decimal lates,int employee_id)
        {
            decimal lates_amount = 0;
            decimal rate_by_hour = CompensationModel.getHourlyRate(employee_id);
            lates_amount += (rate_by_hour / 60) * lates;
            return lates_amount;
        }

        public static decimal getUndertimeComputataion(decimal undertime, int employee_id)
        {
            decimal undertime_amount = 0;
            decimal rate_by_hour = CompensationModel.getHourlyRate(employee_id);
            undertime_amount += (rate_by_hour / 60) * undertime;
            return undertime_amount;
        }
    }
}
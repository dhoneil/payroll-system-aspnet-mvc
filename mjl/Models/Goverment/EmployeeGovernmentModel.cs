using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mjl.Models.Database;

namespace mjl.Models
{
    public class EmployeeGovernmentModel
    {
        public static EmployeeGovernment getEmployeeGovermentDetails(int employee_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            EmployeeGovernment data = db.EmployeeGovernments.Single(s => s.emp_id == employee_id);
            return data;
        }

        public static Boolean verifygetEmployeeGovermentDetails(int employee_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            bool data = db.EmployeeGovernments.Any(s => s.emp_id == employee_id);
            return data;
        }

        public static EmployeeGovernment updateEmployeeGovernment(EmployeeGovernment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            EmployeeGovernment details = db.EmployeeGovernments.Single(s => s.emp_id == data.emp_id);
            //sss
            if (data.sss_amount.HasValue) details.sss_amount = data.sss_amount.Value;
            if (data.sss_type_deduction.HasValue()) details.sss_type_deduction = data.sss_type_deduction;
            //philhealth
            if (data.philhealth_amount.HasValue) details.philhealth_amount = data.philhealth_amount;
            if (data.philhealth_type_deduction.HasValue()) details.philhealth_type_deduction = data.philhealth_type_deduction;
            //pagibig
            if (data.pagibig_amount.HasValue) details.pagibig_amount = data.pagibig_amount;
            if (data.pagibig_type_deduction.HasValue()) details.pagibig_type_deduction = data.pagibig_type_deduction;

            details.last_edit_by = data.last_edit_by;
            details.last_edit_date = data.last_edit_date;

            db.SaveChanges();

            return data;

        }

        public static EmployeeGovernment editGovernment(EmployeeGovernment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            
            
            EmployeeGovernment details = db.EmployeeGovernments.SingleOrDefault(s => s.emp_id == data.emp_id);

            //sss
            if (data.sss_amount.HasValue) 
                    details.sss_amount = data.sss_amount.Value;
            if (data.sss_type_deduction.HasValue()) 
                details.sss_type_deduction = data.sss_type_deduction;
            if (data.with_sss.HasValue) 
                details.with_sss = data.with_sss;

            //philhealth
            if (data.philhealth_amount.HasValue) 
                details.philhealth_amount = data.philhealth_amount;
            if (data.philhealth_type_deduction.HasValue()) 
                details.philhealth_type_deduction = data.philhealth_type_deduction;
            if (data.with_philhealth.HasValue) 
                details.with_philhealth = data.with_philhealth;

            //pagibig
            if (data.pagibig_amount.HasValue) 
                details.pagibig_amount = data.pagibig_amount;
            if (data.pagibig_type_deduction.HasValue()) 
                details.pagibig_type_deduction = data.pagibig_type_deduction;
            if (data.with_pagibig.HasValue) 
                details.with_pagibig = data.with_pagibig;


            details.last_edit_by = data.last_edit_by;
            details.last_edit_date = data.last_edit_date;

            db.SaveChanges();

            return data;
        }

        public static EmployeeGovernment insert(EmployeeGovernment data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            data.prepared_by = Convert.ToInt32(sysSession.UserID);
            data.prepared_date = DateTime.Now;
         
            db.EmployeeGovernments.Add(data);
            db.SaveChanges();

            return data;
        }
    }
}
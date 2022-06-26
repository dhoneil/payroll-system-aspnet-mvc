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
    public class GovernmentController : Controller
    {
        private dbPayrollEntities db = new dbPayrollEntities();
        // GET: Government
        public ActionResult Index()
        {
            return View();
        }
      

        public ActionResult GovernmentDeduction()
        {

            List<Company> companylst = FilterModel.getCompany();
            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ ALL ]]"
            });

            List<Job> joblst = FilterModel.getJob();
            joblst.Add(new Job {
                JobID = 0,
                JobName = "[[ ALL ]]"
            });

            ViewBag.company = new SelectList(companylst.Select(s => new Company { company_id = s.company_id, name = s.name + " ( " + s.type + " ) " }).OrderBy(s => s.company_id), "company_id", "name");
            ViewBag.job = new SelectList(joblst.OrderBy(s=> s.JobID), "JobID", "JobName");

            return View();
        }
        
        public ActionResult viewEmployeeGovernmentDeduction(int company_id, int job_id, int employee_id)
        {
            List<Employee> data = FilterModel.getEmployee(company_id, job_id, employee_id);
            ViewBag.emp_list = data;
            return PartialView();
        }

        public ActionResult updateEmployeeGovernment(List<EmployeeGovernment> employeeGovernmentList)
        {
            foreach (var items in employeeGovernmentList) {

                EmployeeGovernment details = db.EmployeeGovernments.Single(s => s.emp_id == items.emp_id);

                EmployeeGovernment insertData = new EmployeeGovernment();
                insertData.emp_id = items.emp_id;
                insertData.last_edit_by = Convert.ToInt32(sysSession.UserID.ToString());
                insertData.last_edit_date = DateTime.Now;
                //SSS
                if (details.with_sss == true)
                {
                    if (items.sss_type_deduction == "Manual")
                    {
                        insertData.sss_type_deduction = "Manual";
                        insertData.sss_amount = items.sss_amount;
                    }
                    else
                    {
                        insertData.sss_type_deduction = "Auto";
                        insertData.sss_amount = 0;
                    }
                }
                //Philhealth
                if (details.with_philhealth == true)
                {
                    if (items.philhealth_type_deduction == "Manual")
                    {
                        insertData.philhealth_type_deduction = "Manual";
                        insertData.philhealth_amount = items.philhealth_amount;
                    }
                    else
                    {
                        insertData.philhealth_type_deduction = "Auto";
                        insertData.philhealth_amount = 0;
                    }
                }
                //Pagibig
                if (details.with_pagibig == true)
                {
                    if (items.pagibig_type_deduction == "Manual")
                    {
                        insertData.pagibig_type_deduction = "Manual";
                        insertData.pagibig_amount = items.pagibig_amount;
                    }
                    else
                    {
                        insertData.pagibig_type_deduction = "Auto";
                        insertData.pagibig_amount = 0;
                    }
                }
                EmployeeGovernmentModel.updateEmployeeGovernment(insertData);
            }
            return Json(true);
        }

        public ActionResult getSSSTable()
        {
            ViewBag.listSSTable = sssModel.getSSSContribution();
            return PartialView();
        }
    }
}
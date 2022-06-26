using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using mjl.Models.Database;
using mjl.Models;
using mjl.Models.Filter;

namespace mjl.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ScheduleDeduction()
        {
            List<Company> companylst = FilterModel.getCompany();
            companylst.Add(new Company
            {
                company_id = 0,
                name = "[[ ALL ]]"
            });

            List<Job> joblst = FilterModel.getJob();
            joblst.Add(new Job
            {
                JobID = 0,
                JobName = "[[ ALL ]]"
            });

            ViewBag.company = new SelectList(companylst.Select(s => new Company { company_id = s.company_id, name = s.name + " ( " + s.type + " ) " }).OrderBy(s => s.company_id), "company_id", "name");
            ViewBag.job = new SelectList(joblst.OrderBy(s => s.JobID), "JobID", "JobName");


            return View();
        }

        public ActionResult FilterScheduleDeduction(string searchKey, bool show_cancelled)
        {
            List<viewEmployeeCharge> chargelist = ChargeModel.GetData(searchKey, show_cancelled);
            ViewBag.chargelist = chargelist;
            return PartialView();
        }


        public ActionResult SaveChargeEmployee(EmployeeCharge data)
        {
            EmployeeCharge record = new EmployeeCharge();
            record.EmployeeID = data.EmployeeID;
            record.ChargeName = data.ChargeName;
            record.ChargeAmount = data.ChargeAmount;
            record.date_from = data.date_from;
            record.date_to = data.date_to;
            record.prepared_by = Convert.ToInt32(sysSession.UserID);
            record.prepared_date = DateTime.Now;
            record.status = "FOR APPROVAL";

            ChargeModel.insert(record);

            return Json(true);
        }

        public ActionResult UpdateChargeEmployee(EmployeeCharge data)
        {
            EmployeeCharge record = new EmployeeCharge();
            record.ChargeID = data.ChargeID;
            record.ChargeName = data.ChargeName;
            record.ChargeAmount = data.ChargeAmount;
            record.date_from = data.date_from;
            record.date_to = data.date_to;
            record.lastedit_by = Convert.ToInt32(sysSession.UserID);
            record.lastedit_date = DateTime.Now;

            ChargeModel.update(record);

            return Json(true);
        }

        public ActionResult viewEmployeeCharge(int charge_id)
        {
           viewEmployeeCharge emp_charge = ChargeModel.getChargeDataById(charge_id);
           ViewBag.emp_charge = emp_charge;

           return PartialView();
        }

        public ActionResult setChargeReleased(int charge_id)
        {
            ChargeModel.releaseCharge(charge_id);
            return Json(true);
        }

        public ActionResult setChargeCancelled(int charge_id)
        {
            ChargeModel.cancelCharge(charge_id);
            return Json(true);
        }
        
        #region COMPANY
        public ActionResult Companies()
        {
            
            return View();
        }

        public ActionResult FilterCompany(string company, bool show_active)
        {
            List<Company> lstcompany = CompanyModel.GetData(company,show_active);

            ViewBag.lstcompany = lstcompany;

            return PartialView();
        }

        public ActionResult SaveCompany(Company data)
        {
            Company record = new Company();
            record.name = data.name;
            record.owner_name = data.owner_name;
            record.type = data.type;
            record.address = data.address;
            record.city = data.city;
            record.admin_fee = data.admin_fee;
            record.contact_number = data.contact_number;
            record.prepared_by = Convert.ToInt32(sysSession.UserID);
            record.prepared_date = DateTime.Now;
            record.is_active = true;

            CompanyModel.insert(record);

            return Json(true);
        }

        public ActionResult UpdateCompany(Company data)
        {
            Company record = new Company();
            record.company_id = data.company_id;
            record.name = data.name;
            record.owner_name = data.owner_name;
            record.type = data.type;
            record.address = data.address;
            record.city = data.city;
            record.admin_fee = data.admin_fee;
            record.contact_number = data.contact_number;
            record.last_edit_by = Convert.ToInt32(sysSession.UserID);
            record.last_edit_date = DateTime.Now;
            record.is_active = data.is_active;

            CompanyModel.update(record);

            return Json(true);
        }
        #endregion

        #region JOB
        public ActionResult Jobs()
        {

            return View();
        }

        public ActionResult FilterJob(string job_name, bool show_active)
        {
            List<Job> lstjob = JobModel.GetData(job_name, show_active);
            ViewBag.lstjob = lstjob;

            return PartialView();
        }

        public ActionResult SaveJob(Job data)
        {

            Job record = new Job();
            record.JobName = data.JobName;
            record.JobDescription = data.JobDescription;
            record.PreparedBy = Convert.ToInt32(sysSession.UserID);
            record.PreparedDate = DateTime.Now;
            record.IsActive = true;

            JobModel.insert(record);
            return Json(true);
        }

        public ActionResult UpdateJob(Job data)
        {
            Job record = new Job();
            record.JobID = data.JobID;
            record.JobName = data.JobName;
            record.JobDescription = data.JobDescription;
            record.LastEditBy = Convert.ToInt32(sysSession.UserID);
            record.LastEditDate = DateTime.Now;
            record.IsActive = data.IsActive;

            JobModel.update(record);
            return Json(true);
        }
        #endregion;

        #region EMPLOYEE

        public ActionResult Employees()
        {
            List<Company> lstcompany = CompanyModel.GetAllData();
            List<Job> lstjob = JobModel.GetAllData();

            ViewBag.lstjob = lstjob;
            ViewBag.lstcompany = lstcompany;
            ViewBag.prepared_by = Convert.ToInt32(sysSession.UserID);
            return View();
        }
  

        public ActionResult FilterEmployee(int company, bool? is_active, bool? showinactive,int job)
        {
            List<ViewEmployeeV2> lstemployee = EmployeeModel.GetData(company, is_active, showinactive,job);

            ViewBag.lstemployee = lstemployee;

            return PartialView();
        }

        public ActionResult GetCompanyType(int? company_id)
        {
            Company lstcompany = CompanyModel.GetCompanyType(company_id);
            string type = lstcompany.type;
            return Json(new { companytype = type });
        }

        public ActionResult SaveEmployee(string companytype, DateTime probationaryUntil, Employee Employee, EmployeeGovernment EmployeeGovernment, CompensationHistory CompensationHistory, List<EmployeeDependent> dependents)
        {
            DateTime date1 = Convert.ToDateTime(Employee.end_probitionary_date);
            DateTime date2 = DateTime.Now;
            Employee.prob_no_of_months = ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
            //Employee.prob_no_of_months = 1;


            Employee.employee_no = 0;
            var insertEmployee = EmployeeModel.insert(Employee);

            var LAST_INSERTED_EMPLOYEE = insertEmployee.EmployeeID;

            EmployeeGovernment.emp_id = LAST_INSERTED_EMPLOYEE;
            EmployeeGovernment.prepared_by = Convert.ToInt32(sysSession.UserID);
            EmployeeGovernmentModel.insert(EmployeeGovernment);

            CompensationHistory.EmployeeID = LAST_INSERTED_EMPLOYEE;

            if (companytype == "SECURITY")
                CompensationHistory.CompensationType = "MONTHLY";
            else
                CompensationHistory.CompensationType = "DAILY";

            CompensationHistoryModel.insert(CompensationHistory);

            //dependent insertion
            if (dependents != null)
            {
                foreach (var d in dependents)
                {
                    d.emp_id = Convert.ToInt64(LAST_INSERTED_EMPLOYEE);
                    d.preparedBy = Convert.ToInt32(sysSession.UserID);
                    d.preparedDate = DateTime.Now;
                    EmployeeModel.save_EmployeeDependent(d);
                }
            }
            return Json(true);
        }

        public ActionResult EditEmployee_Get(int? empid)
        {
            List<ViewEmployeeV2> emp = EmployeeModel.EditEmployee_Get(empid);
            ViewBag.lstemployee = emp;

            List<Company> lstcompany = CompanyModel.GetAllData();
            ViewBag.lstcompany = lstcompany;

            List<Job> lstjob = JobModel.GetAllData();
            ViewBag.lstjob = lstjob;

            List<EmployeeDependent> lstdependent = EmployeeModel.GetAllemployeeDependent(empid);
            ViewBag.lstdep = lstdependent;

            return PartialView();
        }
        public ActionResult GetAll_EmployeeDependent(int? empid)
        {
            List<EmployeeDependent> lstdependent = EmployeeModel.GetAllemployeeDependent(empid);
            ViewBag.lstdep = lstdependent;
            return PartialView();
        }

        public ActionResult EditEmployee_Post(int ID, string companytype, DateTime probationaryUntil, Employee Employee, EmployeeGovernment EmployeeGovernment, CompensationHistory CompensationHistory,List<EmployeeDependent>dependents)
        {
            Employee data = new Employee();
            EmployeeGovernment empgovdata = new EmployeeGovernment();
            CompensationHistory comphistdata = new CompensationHistory();

            //employee
            data = Employee;
            data.EmployeeID = ID;
            DateTime date1 = probationaryUntil;
            DateTime date2 = DateTime.Now;
            data.prob_no_of_months = ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
            data.LastEditBy = Convert.ToInt32(sysSession.UserID);
            EmployeeModel.updateEmployee(data);

            // employee government

            empgovdata = EmployeeGovernment;
            empgovdata.emp_id = ID;
            empgovdata.last_edit_by = Convert.ToInt32(sysSession.UserID);
            empgovdata.last_edit_date = DateTime.Now;
            EmployeeGovernmentModel.editGovernment(empgovdata);

            //compensation history
            comphistdata = CompensationHistory;
            comphistdata.EmployeeID = ID;
            if (companytype == "SECURITY")
                comphistdata.CompensationType = "MONTHLY";
            else
                comphistdata.CompensationType = "DAILY";
            CompensationHistoryModel.updateCompensationHistory(comphistdata);

            //dependent insertion
            if (dependents != null)
            {
                dbPayrollEntities db = new dbPayrollEntities();

                long thisid = Convert.ToInt64(ID);

                bool isfound = db.EmployeeDependents.Any(s => s.emp_id == thisid);

                if (isfound)
                {
                    foreach (var d in dependents)
                    {
                        d.emp_id = Convert.ToInt64(ID);
                    }
                    EmployeeModel.update_EmployeeDependent(dependents, ID);
                }
                else
                {
                    foreach (var d in dependents)
                    {
                        d.emp_id = Convert.ToInt64(ID);
                        d.preparedBy = Convert.ToInt32(sysSession.UserID);
                        d.preparedDate = DateTime.Now;
                    }
                    EmployeeModel.update_EmployeeDependent(dependents, ID);
                }
            }

            return Json(true);
        }
        public ActionResult ViewEmployeeDetail(int id)
        {
            List<ViewEmployeeV2> emp = EmployeeModel.ViewEmployeeDetail(id);
            ViewBag.lstemployee = emp;
            return PartialView();
        }
        public ActionResult DeactivateEmployee(int empid)
        {
            Employee record = new Employee();
            record.EmployeeID = empid;
            record.IsActive = false;
            EmployeeModel.DeactivateEmployee(record);
            return Json(true);
        }
        public ActionResult ActivateEmployee(int empid)
        {
            Employee record = new Employee();
            record.EmployeeID = empid;
            record.IsActive = true;
            EmployeeModel.ActivateEmployee(record);
            return Json(true);
        }
        public ActionResult CheckEmployeeNumber(int? empno)
        {
            bool data = EmployeeModel.CheckEmployeeNumber(empno);

            return Json(data);
        }
        public ActionResult AddDependent(long emp_id, string name, string relation)
        {
            try
            {
                EmployeeDependent src = new EmployeeDependent();
                src.emp_id = emp_id;
                src.name = name;
                src.relation = relation;
                EmployeeModel.AddDependent(src);
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        public ActionResult RemoveDependent(long id, long emp_id)
        {
            try
            {
                EmployeeDependent src = new EmployeeDependent();
                src.id = id;
                src.emp_id = emp_id;
                EmployeeModel.RemoveDependent(src);

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }


        #endregion
    }
}
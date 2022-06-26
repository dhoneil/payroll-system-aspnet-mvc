using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models;
using mjl.Models.Database;

namespace mjl.Models
{
    public class EmployeeModel
    {

        public static List<ViewEmployeeV2> GetData(int company, bool? is_active, bool? showinactive,int job)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            IQueryable<ViewEmployeeV2> src = db.ViewEmployeeV2;
            if (showinactive == false) { src = src.Where(s => s.IsActive == true); }
            if (company>0) { src = src.Where(s => s.company_id==company); }
            if (job>0) { src = src.Where(s => s.JobID == (job)); }
            if (is_active.HasValue) { src = src.Where(s => s.IsActive == is_active); }


            return src.ToList();
        }

        public static Employee getEmployeeByID(int? employee_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee data = db.Employees.Single(s => s.EmployeeID == employee_id);
            return data;
        }

        public static Employee insert(Employee data)
        {
            //using (var db = new dbPayrollEntities())
            //{
            //    db.Employees.Add(data);
            //    db.SaveChanges();
            //    return data;
            //}

            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                dbPayrollEntities db = new dbPayrollEntities();
                db.Employees.Add(data);
                db.SaveChanges();
                return data;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public static List<ViewEmployeeV2> EditEmployee_Get(int? employee_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<ViewEmployeeV2> data = db.ViewEmployeeV2.Where(s => s.EmployeeID == employee_id);
            return data.ToList();
        }

        public static ViewEmployeeV2 EditEmployee_Get_ByID(int? employee_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            ViewEmployeeV2 data = db.ViewEmployeeV2.SingleOrDefault(s => s.EmployeeID == employee_id);
            return data;
        }

        public static Employee updateEmployee(Employee data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee src = db.Employees.Single(s => s.EmployeeID == data.EmployeeID);

            src.FirstName = data.FirstName;
            src.MiddleName = data.MiddleName;
            src.LastName = data.LastName;
            src.DateHired = data.DateHired;
            src.Gender = data.Gender;
            src.BirthDate = data.BirthDate;
            src.BirthPlace = data.BirthPlace;
            src.JobID = data.JobID;
            //prepared by
            src.LastEditBy = Convert.ToInt32(sysSession.UserID);
            src.LastEditDate = DateTime.Now;
            src.IsActive = data.IsActive;
            src.company_id = data.company_id;
            src.contact_no = data.contact_no;
            src.birth_place = data.birth_place;
            src.height = data.height;
            src.weight = data.weight;
            src.probitionary = data.probitionary;
            src.prob_no_of_months = data.prob_no_of_months;
            src.end_probitionary_date = data.end_probitionary_date;
            src.province_address = data.province_address;
            src.email = data.email;
            src.Current_country = data.Current_country;
            src.MaritalStatus = data.MaritalStatus;
            src.religion = data.religion;
            src.Current_region = data.Current_region;
            src.Current_city_municipality = data.Current_city_municipality;
            src.Current_street = data.Current_street;
            src.Current_barangay = data.Current_barangay;
            src.Current_sitio = data.Current_sitio;
            src.provincial_country = data.provincial_country;
            src.provincial_region = data.provincial_region;
            src.provincial_city_municipality = data.provincial_city_municipality;
            src.provincial_street = data.provincial_street;
            src.provincial_barangay = data.provincial_barangay;
            src.provincial_sitio = data.provincial_sitio;
            src.renew_contract = data.renew_contract;

            src.SSS_Number = data.SSS_Number;
            src.PagIbig_Number = data.PagIbig_Number;
            src.PhilHealth_Number = data.PhilHealth_Number;
            src.TIN_Number = data.TIN_Number;
            src.atm_number = data.atm_number;

            db.SaveChanges();

            return data;
        }

        public static List<ViewEmployeeV2> ViewEmployeeDetail(int id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<ViewEmployeeV2> lst = db.ViewEmployeeV2.Where(s => s.EmployeeID == id);

            return lst.ToList();
        }
        public static Employee DeactivateEmployee(Employee data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee src = db.Employees.Single(s => s.EmployeeID == data.EmployeeID);

            src.IsActive = data.IsActive;

            db.SaveChanges();
            return data;

        }
        public static Employee ActivateEmployee(Employee data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Employee src = db.Employees.Single(s => s.EmployeeID == data.EmployeeID);

            src.IsActive = data.IsActive;

            db.SaveChanges();

            return data;

        }
        public static bool CheckEmployeeNumber(int? id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            bool flag = db.Employees.Any(s => s.employee_no == id);

            return flag;
        }

        public static EmployeeDependent save_EmployeeDependent(EmployeeDependent dependents)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            db.EmployeeDependents.Add(dependents);

            db.SaveChanges();

            return dependents;
        }
        public static List<EmployeeDependent> GetAllemployeeDependent(int? employee_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            IQueryable<EmployeeDependent> data = db.EmployeeDependents.Where(s => s.emp_id == employee_id);
            return data.ToList();
        }

        public static List<EmployeeDependent> update_EmployeeDependent(List<EmployeeDependent> dependents, int ID)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            List<EmployeeDependent> data = db.EmployeeDependents.Where(s => s.emp_id == ID).ToList();
            db.EmployeeDependents.RemoveRange(data);
            
            db.EmployeeDependents.AddRange(dependents);
            db.SaveChanges();
            return dependents;
        }
        public static EmployeeDependent AddDependent(EmployeeDependent src)
        {
            using (var db = new dbPayrollEntities())
            {
                db.EmployeeDependents.Add(src);
                db.SaveChanges();
                return src;
            }
        }
        public static EmployeeDependent RemoveDependent(EmployeeDependent src)
        {
            using (var db = new dbPayrollEntities())
            {
                EmployeeDependent data = db.EmployeeDependents.Single(s => s.id == src.id);
                db.EmployeeDependents.Remove(data);
                db.SaveChanges();
                return data;
            }
        }
    }
}
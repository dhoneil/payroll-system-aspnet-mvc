using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models.Database;

namespace mjl.Models
{
    public class CompanyModel
    {
        public static List<Company> GetAllData()
        {
            dbPayrollEntities db = new dbPayrollEntities();

            IQueryable<Company> lstcompany = db.Companies.Where(s=>s.is_active==true);

            return lstcompany.ToList();
        }
        public static List<Company> GetAllDataByStatus(bool status)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            IQueryable<Company> lstcompany = db.Companies.Where(s => s.is_active == status);

            return lstcompany.ToList();
        }

        public static List<Company> GetData(string company, bool show_inactive)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            IQueryable<Company> lstcompany = db.Companies.Where(s => s.name.Contains(company));
            if (show_inactive == false) { lstcompany = lstcompany.Where(s => s.is_active == true); }
            lstcompany = lstcompany.OrderByDescending(s => s.company_id);
            return lstcompany.ToList();
        }

        public static Company getCompany(int company_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Company data = db.Companies.Single(s => s.company_id == company_id);
            return data;
        }

        public static Company insert(Company data)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            db.Companies.Add(data);
            db.SaveChanges();

            return data;
        }

        public static Company update(Company data)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            Company src = db.Companies.Single(s => s.company_id == data.company_id);

            if (data.name.HasValue()) { src.name = data.name; }
            if (data.type.HasValue()) { src.type = data.type; }
            if (data.owner_name.HasValue()) { src.owner_name = data.owner_name; }
            if (data.address.HasValue()) { src.address = data.address; }
            if (data.city.HasValue()) { src.city = data.city; }
            if (data.contact_number.HasValue()) { src.contact_number = data.contact_number; }
            if (data.is_active.HasValue) { src.is_active = data.is_active; }
            if (data.admin_fee.HasValue) { src.admin_fee = data.admin_fee;  }
            src.last_edit_by = data.last_edit_by;
            src.last_edit_date = data.last_edit_date;

            db.SaveChanges();

            return src;
        }

        public static Company GetCompanyType(int? companyid)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            Company src = db.Companies.Single(s => s.company_id == companyid);
            return src;
        }
    }
}
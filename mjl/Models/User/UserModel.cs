using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mjl.Models;
using mjl.Models.Database;

namespace mjl.Models
{
    public class UserModel
    {
        public static User gerUserById(int user_id)
        {
            dbPayrollEntities db = new dbPayrollEntities();
            User data = db.Users.Where(s => s.UserID == user_id).SingleOrDefault();
            return data;
        }

        public static User editprofile(User data,Employee emp)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            User src = db.Users.Single(s => s.UserID == data.UserID);
            Employee employee = db.Employees.Single(s => s.EmployeeID == emp.EmployeeID);

           
            employee.FirstName = emp.FirstName;
            employee.LastName = emp.LastName;
            employee.email = emp.email;

            src.FirstName = data.FirstName;
            src.LastName = data.LastName;

            src.UserName = data.UserName;
            src.Email = data.Email;
            src.Password = data.Password;

            db.SaveChanges();

            return data;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using mjl.Models;
using mjl.Models.Database;

namespace mjl.Models
{
    public class CompensationHistoryModel
    {
        public static CompensationHistory insert(CompensationHistory data)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            data.IsActive = true;
            //data.PreparedBy = SystemHelper.CurrentUser.UserID;
            data.PreparedDate = DateTime.Now;
            //data.LastEditBy = SystemHelper.CurrentUser.UserID;
            data.LastEditDate = DateTime.Now;

            db.CompensationHistories.Add(data);

            db.SaveChanges();

            return data;
        }
        public static CompensationHistory updateCompensationHistory(CompensationHistory data)
        {
            dbPayrollEntities db = new dbPayrollEntities();

            CompensationHistory src = db.CompensationHistories.Single(s => s.EmployeeID == data.EmployeeID);

            src.ValidityStart = data.ValidityStart;
            src.CompensationType = data.CompensationType;
            src.CompensationRate = data.CompensationRate;

            db.SaveChanges();

            return data;

        }
    }
}
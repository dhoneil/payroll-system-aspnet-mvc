//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mjl.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee_Payslip_Adjustment
    {
        public int payslip_adjustment_id { get; set; }
        public Nullable<int> payslip_id { get; set; }
        public string name { get; set; }
        public Nullable<decimal> amount { get; set; }
        public string type { get; set; }
        public Nullable<int> prepared_by { get; set; }
        public Nullable<System.DateTime> prepared_date { get; set; }
        public Nullable<int> cancelled_by { get; set; }
        public Nullable<System.DateTime> cancelled_date { get; set; }
        public Nullable<int> lastedit_by { get; set; }
        public Nullable<System.DateTime> lastedit_date { get; set; }
        public string status { get; set; }
        public Nullable<bool> isCharges { get; set; }
    }
}

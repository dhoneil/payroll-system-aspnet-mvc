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
    
    public partial class View_billingAdjustments
    {
        public long id { get; set; }
        public Nullable<int> payslip_id { get; set; }
        public string name { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<int> prepared_by { get; set; }
        public Nullable<System.DateTime> prepared_date { get; set; }
        public Nullable<int> last_edit_by { get; set; }
        public Nullable<System.DateTime> last_edit_date { get; set; }
        public Nullable<int> Expr1 { get; set; }
        public Nullable<int> Expr5 { get; set; }
        public Nullable<int> employee_id { get; set; }
        public Nullable<decimal> employee_rate { get; set; }
        public Nullable<System.DateTime> date_from { get; set; }
        public Nullable<System.DateTime> date_to { get; set; }
        public Nullable<decimal> total_additionals { get; set; }
        public Nullable<decimal> total_less { get; set; }
        public Nullable<decimal> total_deduction { get; set; }
        public Nullable<decimal> gross_pay { get; set; }
        public Nullable<decimal> net_pay { get; set; }
        public string status { get; set; }
        public Nullable<int> Expr2 { get; set; }
        public Nullable<System.DateTime> Expr3 { get; set; }
        public Nullable<int> cancelled_by { get; set; }
        public Nullable<System.DateTime> cancelled_date { get; set; }
        public Nullable<int> approved_by { get; set; }
        public Nullable<System.DateTime> approved_date { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string fname_edit { get; set; }
        public string lname_edit { get; set; }
        public Nullable<int> e_ID { get; set; }
    }
}

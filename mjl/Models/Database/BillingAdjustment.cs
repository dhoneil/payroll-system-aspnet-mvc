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
    
    public partial class BillingAdjustment
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
    }
}
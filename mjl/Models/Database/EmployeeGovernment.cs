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
    
    public partial class EmployeeGovernment
    {
        public long goverment_deduction_id { get; set; }
        public Nullable<int> emp_id { get; set; }
        public Nullable<bool> with_sss { get; set; }
        public string sss_type_deduction { get; set; }
        public Nullable<decimal> sss_amount { get; set; }
        public Nullable<bool> with_philhealth { get; set; }
        public string philhealth_type_deduction { get; set; }
        public Nullable<decimal> philhealth_amount { get; set; }
        public Nullable<bool> with_pagibig { get; set; }
        public string pagibig_type_deduction { get; set; }
        public Nullable<decimal> pagibig_amount { get; set; }
        public Nullable<int> prepared_by { get; set; }
        public Nullable<System.DateTime> prepared_date { get; set; }
        public Nullable<int> last_edit_by { get; set; }
        public Nullable<System.DateTime> last_edit_date { get; set; }
        public Nullable<bool> with_tin { get; set; }
    }
}

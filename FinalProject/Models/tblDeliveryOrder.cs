//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblDeliveryOrder
    {
        public int order_num { get; set; }
        public string special_instructions { get; set; }
        public Nullable<System.DateTime> time_order_placed { get; set; }
        public string customer_address { get; set; }
    }
}
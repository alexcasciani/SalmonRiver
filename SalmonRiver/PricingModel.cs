//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalmonRiver
{
    using System;
    using System.Collections.Generic;
    
    public partial class PricingModel
    {
        public int PricingModelID { get; set; }
        public int GuestCount { get; set; }
        public Nullable<System.DateTime> StayDate { get; set; }
        public decimal StayCost { get; set; }
        public string Notes { get; set; }
    }
}

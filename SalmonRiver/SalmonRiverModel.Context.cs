﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SalmonRiverEntities : DbContext
    {
        public SalmonRiverEntities()
            : base("name=SalmonRiverEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Hold> Holds { get; set; }
    }
}

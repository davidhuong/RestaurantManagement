﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RestaurantDB : DbContext
    {
        public RestaurantDB()
            : base("name=RestaurantDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblDeliveryOrder> tblDeliveryOrders { get; set; }
        public virtual DbSet<tblGuestFeedback> tblGuestFeedbacks { get; set; }
        public virtual DbSet<tblGuest> tblGuests { get; set; }
        public virtual DbSet<tblUnusedTable> tblUnusedTables { get; set; }
        public virtual DbSet<tblUsedTable> tblUsedTables { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PropertyRental.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PropertyRentalDBEntities : DbContext
    {
        public PropertyRentalDBEntities()
            : base("name=PropertyRentalDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ApartmentImage> ApartmentImages { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageStatus> MessageStatuses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}

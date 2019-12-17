﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SlotsDataCapture.ENTITIES
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SDCEntities : DbContext
    {
        public SDCEntities()
            : base("name=SDCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BoardAerials> BoardAerials { get; set; }
        public virtual DbSet<BoardBatches> BoardBatches { get; set; }
        public virtual DbSet<BoardImages> BoardImages { get; set; }
        public virtual DbSet<BoardManufacturers> BoardManufacturers { get; set; }
        public virtual DbSet<Boards> Boards { get; set; }
        public virtual DbSet<BoardTypes> BoardTypes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<CourierService> CourierServices { get; set; }
        public virtual DbSet<ImageTypes> ImageTypes { get; set; }
        public virtual DbSet<ManufacturerModels> ManufacturerModels { get; set; }
        public virtual DbSet<Manufacturers> Manufacturers { get; set; }
        public virtual DbSet<Operators> Operators { get; set; }
        public virtual DbSet<OperatorTypes> OperatorTypes { get; set; }
        public virtual DbSet<OperatorVenues> OperatorVenues { get; set; }
        public virtual DbSet<Reports> Reports { get; set; }
        public virtual DbSet<Returns> Returns { get; set; }
        public virtual DbSet<SimCardBatches> SimCardBatches { get; set; }
        public virtual DbSet<SimCardManufacturers> SimCardManufacturers { get; set; }
        public virtual DbSet<SimCards> SimCards { get; set; }
        public virtual DbSet<SimCardTypes> SimCardTypes { get; set; }
        public virtual DbSet<SlotHarnesses> SlotHarnesses { get; set; }
        public virtual DbSet<SlotImages> SlotImages { get; set; }
        public virtual DbSet<SlotModels> SlotModels { get; set; }
        public virtual DbSet<SlotName> SlotNames { get; set; }
        public virtual DbSet<Slots> Slots { get; set; }
        public virtual DbSet<SlotStatus> SlotStatus { get; set; }
        public virtual DbSet<SlotTypes> SlotTypes { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
        public virtual DbSet<Tracker> Trackers { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
    }
}

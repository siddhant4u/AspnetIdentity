﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecurityTrial.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AspNetDatabaseEntities : DbContext
    {
        public AspNetDatabaseEntities()
            : base("name=AspNetDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<SYSTEM_ROLE> SYSTEM_ROLE { get; set; }
        public virtual DbSet<SYSTEM_USER> SYSTEM_USER { get; set; }
        public virtual DbSet<SYSTEM_USER_LOGIN> SYSTEM_USER_LOGIN { get; set; }
        public virtual DbSet<SYSTEM_USER_ROLE> SYSTEM_USER_ROLE { get; set; }
        public virtual DbSet<SYSTEM_USER_CLAIM> SYSTEM_USER_CLAIM { get; set; }
    }
}
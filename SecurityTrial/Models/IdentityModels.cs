﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace SecurityTrial.Models
{
    // You can add profile data for the user by adding more properties to your SYSTEM_USER class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //
    //we have extended SYSTEM_USER to include our new required properties, and added the IdentityManager class, 
    //which includes the methods required to create new users, and add/remove users from available roles.

    public partial class SYSTEM_USER : IdentityUser
    {
      
    }
    public partial class SYSTEM_ROLE : IdentityRole
    { }
    public partial class SYSTEM_USER_ROLE : IdentityUserRole
    { }
    public partial class SYSTEM_USER_LOGIN : IdentityUserLogin
    { }
    public partial class SYSTEM_USER_CLAIM : IdentityUserClaim
    { }

    public class IdentityManager
    {
        // Swap ApplicationRole for IdentityRole:
        RoleManager<SYSTEM_ROLE> _roleManager = new RoleManager<SYSTEM_ROLE>(
            new RoleStore<SYSTEM_ROLE>(new ApplicationDbContext()));

        UserManager<SYSTEM_USER> _userManager = new UserManager<SYSTEM_USER>(
            new UserStore<SYSTEM_USER>(new ApplicationDbContext()));

        ApplicationDbContext _db = new ApplicationDbContext();


        public bool RoleExists(string name)
        {

            return _roleManager.RoleExists(name);
        }

        public bool CreateRole(SYSTEM_ROLE role)
        {

            var idResult = _roleManager.Create(role);
            return idResult.Succeeded;
        }

        public bool CreateUser(SYSTEM_USER user, string password)
        {

            var idResult = _userManager.Create(user, password);
            return idResult.Succeeded;
        }

        public bool AddUserToRole(string userId, SelectRoleEditorViewModel roleName)
        {


            //var idResult = _userManager.AddToRole(userId, roleName.RoleName);
             
            //add new role
            var userRole = new SYSTEM_USER_ROLE
             {
                 UserId = userId,
                 RoleId = _roleManager.FindByName(roleName.RoleName).Id,
                 DateFrom = roleName.DATE_FROM,
                 DateTo = roleName.DATE_TO
             };

            _db.SYSTEM_USER_ROLEs.Add(userRole);
            _db.SaveChanges();



            return true;
        }

        public void ClearUserRoles(string userId)
        {

            var user = _userManager.FindById(userId);
            var currentRoles = new List<SYSTEM_USER_ROLE>();
            currentRoles.AddRange(user.SYSTEM_USER_ROLE);

            var roles = user.SYSTEM_USER_ROLE;


            foreach (var role in currentRoles)
            {
                 
                var result = _userManager.RemoveFromRole(userId, role.SYSTEM_ROLE.Name);
                //user.SYSTEM_USER_ROLE.Remove(role);

            }
            _db.SaveChanges();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<SYSTEM_USER>
    {
        new public virtual IDbSet<SYSTEM_ROLE> Roles { get; set; }
        public DbSet<SYSTEM_USER_ROLE> SYSTEM_USER_ROLEs { get; set; }
        public ApplicationDbContext()
            : base("AspNetDatabaseEntities")
        {
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
              .ToTable("SYSTEM_USER", "dbo").Property(p => p.Id).HasColumnName("USER_ID");
            modelBuilder.Entity<SYSTEM_USER>()
                .ToTable("SYSTEM_USER", "dbo").Property(p => p.Id).HasColumnName("USER_ID");
            modelBuilder.Entity<IdentityUserRole>().ToTable("SYSTEM_USER_ROLE");
            modelBuilder.Entity<SYSTEM_USER_ROLE>().ToTable("SYSTEM_USER_ROLE");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("SYSTEM_USER_LOGIN");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("SYSTEM_USER_CLAIMS");
            modelBuilder.Entity<IdentityRole>().ToTable("SYSTEM_ROLE");
            modelBuilder.Entity<SYSTEM_ROLE>().ToTable("SYSTEM_ROLE");

            

        }
       

    }
}
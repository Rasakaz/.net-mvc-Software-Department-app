using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LabProject.Models;

namespace LabProject.Dal
{
    public class UsersDB: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder) { 
        
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");

        }

        public DbSet<User> Users { get; set; }
       

    }
}
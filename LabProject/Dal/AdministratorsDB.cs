using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LabProject.Models;

namespace LabProject.Dal
{
    public class AdministratorsDB: DbContext
    {
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Administrator>().ToTable("Administrators");

        }
        public DbSet<Administrator> Administrators { get; set; }
    }
}


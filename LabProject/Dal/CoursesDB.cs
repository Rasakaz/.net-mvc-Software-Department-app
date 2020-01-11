using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LabProject.Models;

namespace LabProject.Dal
{
    public class CoursesDB: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().ToTable("Courses");
        }

        public DbSet<Course> Courses { get; set; }

    }
}
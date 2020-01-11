using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LabProject.Models;
using System.Data.Entity;

namespace LabProject.Dal
{
    public class StudentCoursesDB: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentCourses>().ToTable("StudentsCourses");
        }

        public DbSet<StudentCourses> StudentCourses { get; set; }

    }
}
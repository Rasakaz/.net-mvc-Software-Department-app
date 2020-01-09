using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LabProject.Models;

namespace LabProject.Dal
{
    public class CoursesScheduleDB: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CoursesSchedule>().ToTable("CoursesSchedule");
        }

        public DbSet<CoursesSchedule> CoursesSchedules { get; set; }
    }
}
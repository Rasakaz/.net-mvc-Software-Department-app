using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LabProject.Models;

namespace LabProject.Dal
{
    public class LecturerDB: DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Lecturer>().ToTable("Lecturers");
        }

        public DbSet<Lecturer> Lecturers { get; set; }

    }
}


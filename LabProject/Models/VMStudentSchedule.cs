using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabProject.Models
{
    public class VMStudentSchedule
    {
        public List<StudentCourses> Schedule { get; set; }

        public VMStudentSchedule(List<StudentCourses> schedule)
        {
            Schedule = schedule;
        }
    }
}
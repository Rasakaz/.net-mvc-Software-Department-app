using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabProject.Models
{
    public class VMLecturerSchedule
    {
        public List<CoursesSchedule> Schedule { get; set; }

        public VMLecturerSchedule(List<CoursesSchedule> schedule)
        {
            Schedule = schedule;
        }
    }
}
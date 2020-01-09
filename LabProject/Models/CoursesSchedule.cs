using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabProject.Models
{
    public class CoursesSchedule
    {
        public string CourseName { set; get; }

        public string LecturerName { set; get; }
        public string Hour { set; get; }
        public string Day { set; get; }
    }
}
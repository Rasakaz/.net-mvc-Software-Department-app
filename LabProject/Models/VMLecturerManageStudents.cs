using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabProject.Models
{
    public class VMLecturerManageStudents
    {
        public List<string> LectuerCourses { get; set; }
        public List<string> StudentCourse { get; set; }
        public StudentCourses Student { get; set; }

        public VMLecturerManageStudents(List<string> lecturerCourses)
        {
            LectuerCourses = lecturerCourses;
        }

        public IEnumerable<SelectListItem> GetCourses => new SelectList(LectuerCourses);
    }
}
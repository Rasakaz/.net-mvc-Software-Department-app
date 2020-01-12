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
        public List<string> StudentsList { get; set; }
        public List<StudentCourses> Grades { get; set; }
        public StudentCourses Student { get; set; }
        public string StudentName { get; set; }

        public VMLecturerManageStudents(List<string> lecturerCourses)
        {
            LectuerCourses = lecturerCourses;
        }

        public VMLecturerManageStudents(List<string> lecturerCourses, List<string> studentsList)
        {
            LectuerCourses = lecturerCourses;
            StudentsList = studentsList;
        }

        public VMLecturerManageStudents(List<string> lecturerCourses, List<string> studentsList, List<StudentCourses> grades)
        {
            LectuerCourses = lecturerCourses;
            StudentsList = studentsList;
            Grades = grades;
        }

        public VMLecturerManageStudents() { }

        public IEnumerable<SelectListItem> GetCourses => new SelectList(LectuerCourses);
        public IEnumerable<SelectListItem> GetStudents => new SelectList(StudentsList);
    }
}
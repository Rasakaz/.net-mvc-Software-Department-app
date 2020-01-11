using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabProject.Models
{
    public class VManageStudents
    {
        public List<string> StudentsList { get; set; }
        public List<string> CoursesList { get; set; }
        public List<string> LecturerList { get; set; }
        public StudentCourses CourseToAdd { get; set; }


        public VManageStudents (List<string> studentsList, List<string> coursesList, List<string> lectuersList)
        {
            StudentsList = studentsList;
            CoursesList = coursesList;
            LecturerList = lectuersList;
        }

        public VManageStudents() { }

        public IEnumerable<SelectListItem> GetStudentsList => new SelectList(StudentsList);
        public IEnumerable<SelectListItem> GetCoursesList => new SelectList(CoursesList);
        public IEnumerable<SelectListItem> GetLecturersList => new SelectList(LecturerList);
        public IEnumerable<SelectListItem> GetDays => new SelectList(_GetDays());
        public IEnumerable<SelectListItem> GetHours => new SelectList(_GetHours());

        private List<string> _GetHours()
        {
            List<string> hoursList = new List<string>();
            for (int i = 8; i <= 17; i++)
                hoursList.Add(i + ":00");
            return hoursList;
        }


        private List<string> _GetDays()
        {
            return new List<string>(new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" });
        }
    }
}
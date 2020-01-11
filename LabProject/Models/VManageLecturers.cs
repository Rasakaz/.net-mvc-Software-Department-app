using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LabProject.Models
{
    public class VManageLecturers
    {
        public List<string> LecturersList { get; set; }
        public List<string> CoursesList { get; set; }
        public CoursesSchedule CourseToAdd { get; set; }
        public User User { get; set; }
        public Lecturer Lecturer { get; set; }
        public CoursesSchedule LecturerUpdate { get; set; }
       

        public VManageLecturers(List<string> lecturersList, List<string> coursesList)
        {
            LecturersList = lecturersList;
            CoursesList = coursesList;
        }

        public VManageLecturers() { }

        public IEnumerable<SelectListItem> GetLecturerList => new SelectList(LecturersList);

        public IEnumerable<SelectListItem> GetCoursesNames => new SelectList(CoursesList);

        public IEnumerable<SelectListItem> GetCoursesHours => new SelectList(GetHours());

        public IEnumerable<SelectListItem> GetCoursesDays => new SelectList(GetDays());

        private List<string> GetHours() {
            List<string> hoursList = new List<string>();
            for (int i = 8; i <= 17; i++)
                hoursList.Add(i + ":00");
            return hoursList;
        }

        private List<string> GetDays() {
            return new List<string>(new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" });
        }

 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabProject.Dal;
using LabProject.Models;

namespace LabProject.Controllers
{
    public class StudentController : Controller
    {
        // GET: Sutdent
        [HttpGet]
        public ActionResult StudentHome()
        {
            StudentsDB dalStudent = new StudentsDB();
            UsersDB dalUser = new UsersDB();
            string userName = Session["userName"].ToString();


            List<User> objUser =
                (from x in dalUser.Users
                 where (x.UserName == userName)
                 select x).ToList<User>();

            List<Student> objStudent =
                (from x in dalStudent.Students
                 where (x.UserName == userName)
                 select x).ToList<Student>();

            User user = new User
            {
                UserName = objUser[0].UserName,
                Password = objUser[0].Password,
                FirstName = objUser[0].FirstName,
                LastName = objUser[0].LastName,
                Address = objUser[0].Address
            };

            ViewData["selector"] = "information";
            Session["firstName"] = objUser[0].FirstName;
            Session["lastName"] = objUser[0].LastName;

            return View("StudentHome", user);
        }

        public ActionResult StudentSchedule()
        {
            string studentUserName = Session["userName"].ToString();
            
            StudentCoursesDB dalStudent = new StudentCoursesDB();
            
            
            ViewData["selector"] = "schedule";
            return View("StudentScheduleView", new VMStudentSchedule((from x in dalStudent.StudentCourses where x.UserName.Contains(studentUserName) select x).ToList<StudentCourses>()));
        }

  
        public ActionResult StudentExams()
        {
            string studentUserName = Session["userName"].ToString();
            var courses = (from x in (new StudentCoursesDB()).StudentCourses where
                            x.UserName.Contains(studentUserName)
                           select x).ToList();

            List<Course> allCourses = (from x in (new CoursesDB()).Courses select x).ToList<Course>();
            List<Course> studentCourses = new List<Course>();
            
            foreach(var a in allCourses)
            {
                foreach(var b in courses)
                {
                    if(a.CourseName == b.CourseName)
                    {
                        studentCourses.Add(a);
                        break;
                    }
                }
            }
            ViewData["selector"] = "examSchedule";
            return View("StudentExam", new VMExamSchedule(studentCourses));
        }

    }
}
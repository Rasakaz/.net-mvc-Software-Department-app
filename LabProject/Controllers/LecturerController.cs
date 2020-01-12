using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabProject.Models;
using LabProject.Dal;

namespace LabProject.Controllers
{
    public class LecturerController : Controller
    {
        // GET: Lecturer
        [HttpGet]
        public ActionResult LecturerHome()
        {
            LecturerDB dalLec = new LecturerDB();
            UsersDB dalUser = new UsersDB();
            //List<User> userObj = (from x in dalUser.Users where( x.UserName == Session["userName"].ToString()) select x).ToList<User>();

            string userName = Session["userName"].ToString();
            List<User> objUser =
                (from x in dalUser.Users
                 where (x.UserName == userName)
                 select x).ToList<User>();

            List<Lecturer> objLecturer =
                (from x in dalLec.Lecturers
                 where (x.UserName == userName)
                 select x).ToList<Lecturer>();
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
            ViewData["salary"] = objLecturer[0].Salary;
            return View("LecturerHome", user);
        }

        public ActionResult LecturerSchedule()
        {
            string lecturerUserName = Session["userName"].ToString();
            List<User> lecturer = (from x in (new UsersDB()).Users
                                  where x.UserName == lecturerUserName
                                  select x).ToList<User>();

            string lecturerName = lecturer[0].FirstName + " " + lecturer[0].LastName;
            CoursesScheduleDB dalSchedule = new CoursesScheduleDB();
            
            VMLecturerSchedule schedule = new VMLecturerSchedule((from x in dalSchedule.CoursesSchedules
                                              where x.LecturerName.Contains(lecturerName)
                                              select x).ToList<CoursesSchedule>());
            
            ViewData["selector"] = "schedule";
            return View("LecturerScheduleView", schedule);


        }

        private List<string> GetLecturersCourses()
        {
            string lecturerUserName = Session["userName"].ToString();
            List<User> lecturer = (from x in (new UsersDB()).Users
                                   where x.UserName == lecturerUserName
                                   select x).ToList<User>();

            string lecturerName = lecturer[0].FirstName + " " + lecturer[0].LastName;
            CoursesScheduleDB dalSchedule = new CoursesScheduleDB();
            List<string> lecturerCourses = (from x in dalSchedule.CoursesSchedules
                                            where x.LecturerName == lecturerName
                                            select x.CourseName).Distinct().ToList<string>();
            return lecturerCourses;
        }

        public ActionResult LecturerManageStudents()
        {
            ViewData["selector"] = "manage_Students";
            return View("LecturerManageStudents", new VMLecturerManageStudents(GetLecturersCourses()));
        }

        [HttpPost]
        public ActionResult GetCourses(VMLecturerManageStudents obj)
        {
            if(obj.Student.CourseName == null)
            {
                TempData["message"] = "select course name";
                return RedirectToAction("LecturerManageStudents", "Lecturer");
            }
            
            Session["courseName"] = obj.Student.CourseName;

            ViewData["selector"] = "manage_Students";
            return View("LecturerManageStudents", new VMLecturerManageStudents(GetLecturersCourses(), GetStudentsNames()));

        }

        private List<string> GetStudentsNames()
        {

            string lecturerUserName = Session["userName"].ToString();
            string courseName = Session["courseName"].ToString();
            UsersDB dalUser = new UsersDB();
            List<User> lecturer = (from x in dalUser.Users
                                   where x.UserName == lecturerUserName
                                   select x).ToList<User>();

            string lecturerName = lecturer[0].FirstName + " " + lecturer[0].LastName;

            StudentCoursesDB dalStudentCourse = new StudentCoursesDB();
            List<string> userNameList = (from x in dalStudentCourse.StudentCourses
                                         where x.CourseName == courseName && x.LecturerName == lecturerName
                                         select x.UserName).Distinct().ToList<string>();

            List<User> allUsersList = (from x in dalUser.Users select x).ToList<User>();

            List<string> studentsNames = new List<string>();

            foreach (var a in allUsersList)
            {
                foreach (var b in userNameList)
                {
                    if (a.UserName.Equals(b))
                    {
                        studentsNames.Add(a.FirstName + " " + a.LastName);
                        break;
                    }
                }
            }
            return studentsNames;
        }

        [HttpPost]
        public ActionResult GetStudents(VMLecturerManageStudents VMobj)
        {
            ViewData["selector"] = "manage_Students";
            if (VMobj.StudentName == null)
            {
                TempData["message"] = "select student name";
                return View("LecturerManageStudents", new VMLecturerManageStudents(GetLecturersCourses(), GetStudentsNames()));
            }
            string firstName = VMobj.StudentName.Substring(0, VMobj.StudentName.IndexOf(" "));
            string lastName = VMobj.StudentName.Substring(VMobj.StudentName.IndexOf(" ") + 1);
            Session["studentFirstName"] = firstName;
            Session["studentLastName"] = lastName;
            string courseName = Session["courseName"].ToString();
            UsersDB dalUser = new UsersDB();
            List<User> userName = (from x in dalUser.Users where x.FirstName == firstName && x.LastName == lastName select x).ToList<User>();
            string sUserName = userName[0].UserName;
            List<StudentCourses> grades = (from x in (new StudentCoursesDB()).StudentCourses
                                           where x.UserName == sUserName &&
                                           x.CourseName == courseName
                                           select x).ToList<StudentCourses>();

            return View("LecturerManageStudents", new VMLecturerManageStudents(GetLecturersCourses(), GetStudentsNames(), grades));
        }

        [HttpPost]
        public ActionResult UpdateGrade(VMLecturerManageStudents VMobj)
        {
            string firstName = Session["studentFirstName"].ToString();
            string lastName = Session["studentLastName"].ToString();
            string courseName = Session["courseName"].ToString();

            UsersDB daluser = new UsersDB();
            List<User> uName = (from x in daluser.Users
                                where x.FirstName == firstName &&
                                 x.LastName == lastName
                                select x).ToList<User>();
            string sUserName = uName[0].UserName;

            StudentCoursesDB dal = new StudentCoursesDB();
            List<StudentCourses> grades = (from x in dal.StudentCourses
                                           where x.UserName == sUserName &&
                                           x.CourseName == courseName
                                           select x).ToList<StudentCourses>();
            if(VMobj.Student.MoedAGrade <= 0 && VMobj.Student.MoedBGrade <= 0)
            {
                TempData["message"] = "Enter grade to update";
                return View("LecturerManageStudents", new VMLecturerManageStudents(GetLecturersCourses(), GetStudentsNames(), grades));
            }
            if (VMobj.Student.MoedAGrade > 0 && VMobj.Student.MoedAGrade <= 100)
                grades[0].MoedAGrade = VMobj.Student.MoedAGrade;
            if(VMobj.Student.MoedBGrade > 0 && VMobj.Student.MoedBGrade <= 100)
                grades[0].MoedBGrade = VMobj.Student.MoedBGrade;
            dal.SaveChanges();
            return View("LecturerManageStudents", new VMLecturerManageStudents(GetLecturersCourses(), GetStudentsNames(), grades));
        }
    }



}

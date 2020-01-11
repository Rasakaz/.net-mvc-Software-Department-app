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

        public ActionResult LecturerManageStudents()
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
         
            ViewData["selector"] = "manage_Students";
            return View("LecturerManageStudents", new VMLecturerManageStudents(lecturerCourses));
        }

        public ActionResult GetCourses(VMLecturerManageStudents obj)
        {

            ViewData["selector"] = "schedule";
            return RedirectToAction("LecturerManageStudents", "Lecturer");

        }
    }
}
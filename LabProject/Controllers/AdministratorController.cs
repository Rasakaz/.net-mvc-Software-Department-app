using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabProject.Models;
using LabProject.Dal;


namespace LabProject.Controllers
{
    public class AdministratorController : Controller


    {
        // GET: Administrator
        [HttpGet]
        public ActionResult Index()
        {
            
            string userName = Session["userName"].ToString();
            UsersDB dalUser = new UsersDB();
            List<User> objUser =
                (from x in dalUser.Users
                 where (x.UserName == userName)
                 select x).ToList<User>();

            AdministratorsDB dalAdmin = new AdministratorsDB();
            List<Administrator> objAdministratos =
             (from x in dalAdmin.Administrators
              where (x.UserName == userName)
              select x).ToList<Administrator>();

            ViewData["salary"] = objAdministratos[0].Salary.ToString(); 

            User user = new User
            {
                UserName = objUser[0].UserName,
                Password = objUser[0].Password,
                FirstName = objUser[0].FirstName,
                LastName = objUser[0].LastName,
                Address = objUser[0].Address
            };
            Session["firstName"] = user.FirstName;
            Session["lastName"] = user.LastName;
            ViewData["selector"] = "information";
            return View("AdministratorHome", user);
        }

        public ActionResult ManageLecturers()
        {
            ViewData["selector"] = "manage_Lecturers";
            
            UsersDB dalUser = new UsersDB();
            LecturerDB dalLecture = new LecturerDB();

            var objLecturers =
            (from x in dalLecture.Lecturers
             select x.UserName).ToList();

            List<User> objUsers =
                (from x in dalUser.Users
                 where (objLecturers.Contains(x.UserName))
                 select x).ToList<User>();


            return View("manageLecturersView", objUsers);
        }


        public ActionResult AddCourseToLecturer()
        {
            // the way to get the values from the form by the actual name of the element Request.Form["element name"]
            TempData["actionResult"] = "The course was successfully added";
            return RedirectToAction("ManageLecturers", "Administrator");
        }
    }
}
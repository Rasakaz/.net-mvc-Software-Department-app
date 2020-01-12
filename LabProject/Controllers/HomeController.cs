using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabProject.Models;
using LabProject.Dal;

namespace LabProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["firstName"] = null;
            return View();
        }


        private bool CheckAdministrator(User user)
        {
            AdministratorsDB dal = new AdministratorsDB();
            List<Administrator> objAdministratos =
                (from x in dal.Administrators
                 where (x.UserName == user.UserName)
                 select x).ToList<Administrator>();
            return (objAdministratos.Count == 1);
        }

        private bool CheckLecturer(User user)
        {
            LecturerDB dalLec = new LecturerDB();
            return ((from x in dalLec.Lecturers where x.UserName == user.UserName select x).ToList().Count == 1);
        }

        private bool CheckStudent(User user)
        {
            StudentsDB dal = new StudentsDB();
            return ((from x in dal.Students where x.UserName == user.UserName select x).ToList().Count == 1);
        }

        public ActionResult Submit(User user, string UserType)
        {

            //check if user type have choosen
            if (UserType.Equals(""))
            {
                TempData["errorMessage"] = "please choose user type";
                user = new User();
                return View("Index");
            }

            //check the text boxes validation 
            if (ModelState.IsValidField("UserName") && ModelState.IsValidField("Password"))
            {

                UsersDB dal = new UsersDB();
                List<User> objUsers =
                    (from x in dal.Users
                     where (x.UserName == user.UserName
                        && x.Password == user.Password)
                     select x).ToList<User>();

                if (objUsers.Count == 1) //check the user name and password correct
                {
                    switch (UserType)
                    {
                        case "administrator":
                            if (CheckAdministrator(user))
                            {
                                Session["userName"] = user.UserName;
                                return RedirectToAction("Index", "Administrator");
                            }
                            break;

                        case "lecturer":
                            if (CheckLecturer(user))
                            {
                                Session["userName"] = user.UserName;
                                return RedirectToAction("LecturerHome", "Lecturer");
                            }
                            break;

                        case "student":
                            if (CheckStudent(user))
                            {
                                Session["userName"] = user.UserName;
                                return RedirectToAction("StudentHome", "Student");
                            }
                            break;
                        default:
                            return View("StudentHome", "Sutdent");





                    }

                    TempData["errorMessage"] = "user name / password incorrect";
                    user = new User();
                    return View("Index");
                }
   
            }
            return View("Index");
        }

    }
}



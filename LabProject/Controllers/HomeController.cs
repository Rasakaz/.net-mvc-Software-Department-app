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
            return true;
        }

        private bool CheckStudent(User user)
        {
            return true;
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
            if (ModelState.IsValid)
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
                                return View("ShowUserLog");
                            break;

                        case "student":
                            if (CheckStudent(user))
                                return View("ShowUserLog");
                            break;
                        default:
                            return View("ShowUserLog");

                    }

                    return View("ShowUserLog", user);
                }
                TempData["errorMessage"] = "user name / password incorrect";
                user = new User();
                return View("Index");
            }
            return View("Index");
        }

    }
}

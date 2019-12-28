using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabProject.Models;

namespace LabProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Submit(User user)
        {
            if (ModelState.IsValid)
            {
                Console.Write("ok");
                ViewBag.uName = user.UserName;

                return View("ShowUserLog");
            }
            Console.Write("not");
            return View("Index");
        }

    }
}
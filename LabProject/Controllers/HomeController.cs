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
                ViewBag["userName"] = user.userName;
                ViewBag["password"] = user.password;
            }
            return View();
        }

    }
}
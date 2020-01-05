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
            
            string userName = TempData["userName"].ToString();
            UsersDB dal = new UsersDB();
            List<User> objUser =
                (from x in dal.Users
                 where (x.UserName == userName)
                 select x).ToList<User>();


            User user = new User
            {
                UserName = objUser[0].UserName,
                Password = objUser[0].Password,
                FirstName = objUser[0].FirstName,
                LastName = objUser[0].LastName,
                Address = objUser[0].Address
            };
            
            return View("AdministratorHome", user);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LabProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "AddCourseToLecturer",
                url: "Administrator/manageLecturers/AddCourseToLecturer",
                defaults: new { controller = "Administrator", action = "AddCourseToLecturer", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "manageLecturers",
                url: "Administrator/manageLecturers",
                defaults: new { controller = "Administrator", action = "ManageLecturers", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdministratorHome",
                url: "Administrator",
                defaults: new { controller = "Administrator", action = "Index", id = UrlParameter.Optional }
            );


                routes.MapRoute(
                name: "HomePage",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            

        }


    }
}

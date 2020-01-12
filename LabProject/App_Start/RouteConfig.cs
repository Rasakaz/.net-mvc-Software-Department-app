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
                url: "Administrator/ManageLecturers/AddCourseToLecturer",
                defaults: new { controller = "Administrator", action = "AddCourseToLecturer", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "StudentExams",
               url: "Student/StudentExams",
               defaults: new { controller = "Student", action = "StudentExams", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "StudentSchedule",
                url: "Student/StudentSchedule",
                defaults: new { controller = "Student", action = "StudentSchedule", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LecturerManageStudents",
                url: "Lecturer/LecturerManageStudents",
                defaults: new { controller = "Lecturer", action = "LecturerManageStudents", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "manageCourses",
            url: "Administrator/ManageCourses",
            defaults: new { controller = "Administrator", action = "ManageCourses", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "manageStudents",
                url: "Administrator/ManageStudents",
                defaults: new { controller = "Administrator", action = "ManageStudents", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "manageLecturers",
                url: "Administrator/ManageLecturers",
                defaults: new { controller = "Administrator", action = "ManageLecturers", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LecturerSchedule",
                url: "Lecturer/ManageLecturers/LecturerSchedule",
                defaults: new { controller = "Administrator", action = "LecturerSchedule", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "StudentHome",
                url: "Student",
                defaults: new { controller = "Student", action = "StudentHome", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "LecturerrHome",
              url: "Lecturer",
              defaults: new { controller = "Lecturer", action = "LecturerHome", id = UrlParameter.Optional }
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

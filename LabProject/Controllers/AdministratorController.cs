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
            

            UsersDB dalUser = new UsersDB();
            LecturerDB dalLecture = new LecturerDB();
            CoursesDB dalCourses = new CoursesDB();

            var objLecturers =
            (from x in dalLecture.Lecturers
             select x.UserName).ToList();

            List<string> objUsers =
                (from x in dalUser.Users
                 where (objLecturers.Contains(x.UserName))
                 select (x.FirstName + " " + x.LastName)).ToList<string>();

            List<string> objCourses =
                (from x in dalCourses.Courses
                 select x.CourseName).ToList();

            VManageLecturers manageLecturersObj = new VManageLecturers(objUsers, objCourses);
            ViewData["selector"] = "manage_Lecturers";
            return View("manageLecturersView", manageLecturersObj);
        }

        private bool AddLecturer(VManageLecturers obj)
        {
            LecturerDB dal = new LecturerDB();
            UsersDB dalUser = new UsersDB();
            if ((from x in dal.Lecturers where (x.UserName == obj.User.UserName) select x).ToList().Count == 1)
                return false;
            obj.Lecturer.UserName = obj.User.UserName;
            dal.Lecturers.Add(obj.Lecturer);
            dal.SaveChanges();
            dalUser.Users.Add(obj.User);
            dalUser.SaveChanges();
            return true;
        }

        private bool UpdateLecturer(VManageLecturers obj)
        {
            LecturerDB dalLecturer = new LecturerDB();
            UsersDB dalUser = new UsersDB();
            if ((from x in dalLecturer.Lecturers where (x.UserName == obj.User.UserName) select x).ToList().Count == 0)
                return false;
            var user = (from x in dalUser.Users where obj.User.UserName == x.UserName select x).ToList();
            user[0].Password = obj.User.Password;
            user[0].FirstName = obj.User.FirstName;
            user[0].LastName = obj.User.LastName;
            user[0].Address = obj.User.Address;
            dalUser.SaveChanges();
            var lecturer = (from x in dalLecturer.Lecturers where obj.User.UserName == x.UserName select x).ToList();
            lecturer[0].Salary = Double.Parse(obj.Lecturer.Salary.ToString());
            dalLecturer.SaveChanges();
            return true;
        
        }

        public ActionResult AddUpdateLecturer(VManageLecturers LecturerObj)
        {
            string AddUpdateValue = Request.Form["AddUpdateValue"];
            if (ModelState.IsValid)
            { 
                switch (AddUpdateValue)
                {
                    case "add":
                        if (AddLecturer(LecturerObj))
                        {
                            TempData["message"] = "user added successfully";
                            break;
                        }
                        TempData["message"] = "user already exist in the system";
                        break;

                    case "update":
                        if(UpdateLecturer(LecturerObj))
                        {
                            TempData["message"] = "user successfully updated";
                            break;
                        }
                        TempData["message"] = "user does not exist in the system!";
                        break;
                }
            }
            
            UsersDB dalUser = new UsersDB();
            LecturerDB dalLecture = new LecturerDB();
            CoursesDB dalCourses = new CoursesDB();

            var objLecturers =
            (from x in dalLecture.Lecturers
             select x.UserName).ToList();

            List<string> objUsers =
                (from x in dalUser.Users
                 where (objLecturers.Contains(x.UserName))
                 select (x.FirstName + " " + x.LastName)).ToList<string>();

            List<string> objCourses =
                (from x in dalCourses.Courses
                 select x.CourseName).ToList();

            VManageLecturers manageLecturersObj = new VManageLecturers(objUsers, objCourses);
            ViewData["selector"] = "manage_Lecturers";
            return View("manageLecturersView", manageLecturersObj);


        }

        public ActionResult AddCourseToLecturer(VManageLecturers course)
        {
            var check = true;
            string addUpdate = Request.Form["AddUpdateValue"];

            if (course.CourseToAdd.LecturerName == null) 
            {
                check = false;
                TempData["v1"] = "please select lecturer name";
            }

            if(course.CourseToAdd.CourseName == null)
            {
                check = false;
                TempData["v2"] = "please select course name";
            }

            if (course.CourseToAdd.Day == null)
            {
                check = false;
                TempData["v3"] = "please select course day";
            }

            if (course.CourseToAdd.Hour == null)
            {
                check = false;
                TempData["v4"] = "please select start hour";
            }

            if(check == false)
                return RedirectToAction("ManageLecturers", "Administrator");

            CoursesScheduleDB dalCourses = new CoursesScheduleDB();
            var lecturer = (from x in dalCourses.CoursesSchedules
                            where
(                            x.Hour == course.CourseToAdd.Hour &&
                               x.Day == course.CourseToAdd.Day &&
                                course.CourseToAdd.LecturerName == x.LecturerName)
                            select x).ToList();
            if (lecturer.Count == 1 && addUpdate.Equals("add"))
            {
                check = false;
                TempData["v5"] = "The course exists for the lecturer";
            }

            if(check == false)
                return RedirectToAction("ManageLecturers", "Administrator");

            if (addUpdate.Equals("add"))
            { 
                dalCourses.CoursesSchedules.Add(course.CourseToAdd);
                dalCourses.SaveChanges();
                TempData["message"] = "Course successfully added";
                return RedirectToAction("ManageLecturers", "Administrator");
            }

            var updateLec = (from x in dalCourses.CoursesSchedules
                          where
                             x.LecturerName == course.CourseToAdd.LecturerName &&
                            x.CourseName == course.CourseToAdd.CourseName
                          select x).ToList();

            if (addUpdate.Equals("update") && updateLec.Count == 1)
            {
                updateLec[0].Day = course.LecturerUpdate.Day;
                updateLec[0].Hour = course.LecturerUpdate.Hour;
                TempData["message"] = "Course updated successfully";
                dalCourses.SaveChanges();
            }

            if (updateLec.Count == 0)
            {
                TempData["v5"] = "Lecturer doesnt have the course";
                return RedirectToAction("ManageLecturers", "Administrator");
            }

            return RedirectToAction("ManageLecturers", "Administrator");
        }

        public ActionResult AddCourseToStudent(VManageStudents course)
        {

            var check = true;
            string addUpdateValue = Request.Form["AddUpdateValue"];


            if (course.CourseToAdd.UserName == null)
            {
                check = false;
                TempData["v1"] = "please select student name";
            }
            if(course.CourseToAdd.CourseName == null)
            {
                check = false;
                TempData["v2"] = "please select course name";
            }

            if(course.CourseToAdd.LecturerName == null && addUpdateValue.Equals("add"))
            {
                check = false;
                TempData["v3"] = "please select lecturer name";
            }

            if(course.CourseToAdd.Day == null && addUpdateValue.Equals("add"))
            {
                check = false;
                TempData["v4"] = "please select course day";
            }

            if(course.CourseToAdd.Hour == null && addUpdateValue.Equals("add"))
            {
                check = false;
                TempData["v5"] = "please select course hour";
            }


            if(check == false)
                return RedirectToAction("ManageStudents", "Administrator");

            course.CourseToAdd.UserName = (from x in (new UsersDB()).Users where
                                            (course.CourseToAdd.UserName.Substring(0, course.CourseToAdd.UserName.IndexOf(" ")) == x.FirstName &&
                                            course.CourseToAdd.UserName.Substring(course.CourseToAdd.UserName.IndexOf(",") + 2) == x.Address
                                            )select x.UserName).ToList()[0];
            if (addUpdateValue.Equals("add"))
            {

                //add
                if((from x in(new CoursesScheduleDB()).CoursesSchedules where
                    (x.Hour == course.CourseToAdd.Hour && x.Day == course.CourseToAdd.Day && course.CourseToAdd.LecturerName == x.LecturerName)select x).ToList().Count == 0)
                {
                    check = false;
                    TempData["v6"] = "The course in not avialable!";
                }
                //down need to check if the student take the course
                if ((from x in (new StudentCoursesDB()).StudentCourses where (course.CourseToAdd.UserName == x.UserName &&
                      course.CourseToAdd.CourseName == x.CourseName) select x).ToList().Count == 1)
                {
                    check = false;
                    TempData["v7"] = "The student is already  take that course!";
                }
            
            }
            if (check && addUpdateValue.Equals("add"))
            {
               
                StudentCoursesDB dalStudentCourse = new StudentCoursesDB();
                dalStudentCourse.StudentCourses.Add(course.CourseToAdd);
                dalStudentCourse.SaveChanges();
                TempData["message"] = "Course successfully added";
                return RedirectToAction("ManageStudents", "Administrator");
            }

            if (addUpdateValue.Equals("update"))
            {
                StudentCoursesDB dalStudentCourse = new StudentCoursesDB();
                var student = (from x in dalStudentCourse.StudentCourses
                               where (course.CourseToAdd.UserName == x.UserName &&
                                 course.CourseToAdd.CourseName == x.CourseName)
                               select x).ToList();
                if (student.Count == 0)
                {
                    check = false;
                    TempData["v7"] = "The student does not have the course";
                }

                if(course.CourseToAdd.MoedAGrade <= 0 && course.CourseToAdd.MoedBGrade <= 0)
                {
                    check = false;
                    TempData["v7"] = "No grade enter!";
                }

                if (check == false)
                    return RedirectToAction("ManageStudents", "Administrator");
                if (course.CourseToAdd.MoedAGrade > 0)
                    student[0].MoedAGrade = course.CourseToAdd.MoedAGrade;
                if (course.CourseToAdd.MoedBGrade > 0)
                    student[0].MoedBGrade = course.CourseToAdd.MoedBGrade;
                dalStudentCourse.SaveChanges();
                TempData["message"] = "Grade successfully update";

            }
            return RedirectToAction("ManageStudents", "Administrator");
        }

        public ActionResult ManageStudents()
        {
            StudentsDB dalStudent = new StudentsDB();
            UsersDB dalUser = new UsersDB();
            CoursesDB dalCourses = new CoursesDB();
            LecturerDB dalLecture = new LecturerDB();

            var objStudents =
            (from x in dalStudent.Students
            select x.UserName).ToList();

            List<string> objUsers =
                (from x in dalUser.Users
                 where (objStudents.Contains(x.UserName))
                 select (x.FirstName + " " + x.LastName + ", " + x.Address)).ToList<string>();

            List<string> objCourses =
                (from x in dalCourses.Courses
                 select x.CourseName).ToList();

            var objLecturers =
            (from x in dalLecture.Lecturers
             select x.UserName).ToList();

            List<string> objUserLecturers =
                  (from x in dalUser.Users
                   where (objLecturers.Contains(x.UserName))
                   select (x.FirstName + " " + x.LastName)).ToList<string>();

            VManageStudents manageStudentObj = new VManageStudents(objUsers, objCourses, objUserLecturers);
            ViewData["selector"] = "manage_Students"; //set the selctor that that called for the action
            return View("manageStudentsView", manageStudentObj);
        }

        public ActionResult ManageCourses()
        {
            ViewData["selector"] = "manage_Exams";
            return View("manageCoursesView");
        }

        public ActionResult CourseAddUpdate(Course course)
        {
           bool check = true;
           string addUpdate = Request.Form["AddUpdateValue"];
            CoursesDB dalCourse = new CoursesDB();
           if(course.CourseName.Equals("Course name"))
            {
                check = false;
                TempData["v1"] = "Enter course name";
            }

            if (addUpdate.Equals("add"))
            {
               if(course.MoedADate.Year == 1 && course.MoedADate.Month == 1 && course.MoedADate.Day == 1)
               {
                    check = false;
                    TempData["v2"] = "Enter first exam date";
               }

               if (course.MoedBDate.Year == 1 && course.MoedBDate.Month == 1 && course.MoedBDate.Day == 1)
                {
                    check = false;
                    TempData["v3"] = "Enter second exam date";
                }

               if(check == false)
                    return RedirectToAction("ManageCourses", "Administrator");
                dalCourse.Courses.Add(course);
                dalCourse.SaveChanges();
                TempData["v2"] = "Course added successfully";
                return RedirectToAction("ManageCourses", "Administrator");
            }
            if (check == false)
                return RedirectToAction("ManageCourses", "Administrator");
            if (addUpdate.Equals("update"))
            {
                var courseToUpdate = (from x in dalCourse.Courses where x.CourseName == course.CourseName select x).ToList();
                if (courseToUpdate.Count == 0)
                {
                    TempData["v2"] = "The course does not exist in the system";
                    return RedirectToAction("ManageCourses", "Administrator");
                }

                if (!(course.MoedADate.Year == 1 && course.MoedADate.Month == 1 && course.MoedADate.Day == 1))
                    courseToUpdate[0].MoedADate = course.MoedADate;
                if (!(course.MoedBDate.Year == 1 && course.MoedBDate.Month == 1 && course.MoedBDate.Day == 1))
                    courseToUpdate[0].MoedBDate = course.MoedBDate;
                dalCourse.SaveChanges();
                TempData["v2"] = "Course update successfully";
                return RedirectToAction("ManageCourses", "Administrator");
            }

            return RedirectToAction("ManageCourses", "Administrator");
            
           
        }
    }

}
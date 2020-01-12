using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabProject.Models
{
    public class StudentCourses
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string CourseName { get; set; }
        public int MoedAGrade { get; set; }
        public int MoedBGrade { get; set; }
        public string LecturerName { get; set; }
        public string Day { get; set; }
        public string Hour { get; set; }
    }
}
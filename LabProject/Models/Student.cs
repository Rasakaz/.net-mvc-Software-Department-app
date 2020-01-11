using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabProject.Models
{
    public class Student
    {
        [Key]
        public string UserName { get; set; }
    }
}
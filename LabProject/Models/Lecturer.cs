using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class Lecturer
    {
        [Key]
        public string UserName { set; get; }

        [Required(ErrorMessage = "field is must")]
        public double Salary { set; get; }
    }
}
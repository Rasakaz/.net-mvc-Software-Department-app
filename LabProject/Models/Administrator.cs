using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace LabProject.Models
{
    public class Administrator: DbContext
    {
        [Key]
        public string UserName { get; set; }

        public double Salary { get; set; }



    }
}
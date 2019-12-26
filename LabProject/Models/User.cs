using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class User
    {

        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }
    }
}

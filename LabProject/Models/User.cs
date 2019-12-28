using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Models
{
    public class User
    {

        [Required(ErrorMessage = "field is must")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "field is must")]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabProject.Models
{
    public class VMExamSchedule
    {
        public List<Course> ExamSchedule { get; set; }

        public VMExamSchedule(List<Course> schedule)
        {
            ExamSchedule = schedule;
        }
    }
}
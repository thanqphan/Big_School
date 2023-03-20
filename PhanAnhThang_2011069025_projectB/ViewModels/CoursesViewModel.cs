using PhanAnhThang_2011069025_projectB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhanAnhThang_2011069025_projectB.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<Course> UpcommingCourses { get; set; }
        public bool ShowAction { get; set; }
    }
}
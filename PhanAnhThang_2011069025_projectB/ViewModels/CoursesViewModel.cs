using Microsoft.AspNet.Identity;
using PhanAnhThang_2011069025_projectB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhanAnhThang_2011069025_projectB.ViewModels
{
    public class CoursesViewModel : Controller
    {
        private ApplicationDbContext _dbContext;
        public IEnumerable<Course> UpcommingCourses { get; set; }
        public bool ShowAction { get; set; }
        [Authorize]
        public ActionResult Attending()
        {
            var userId= User.Identity.GetUserId();
            var courses=_dbContext.Attendances
                .Where(a=>a.AttendeeId== userId)
                .Select(a=>a.Course)
                .Include(l=>l.Lecturer)
                .Include(l=>l.Category)
                .ToList();
            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId= User.Identity.GetUserId();
            var course=_dbContext.Course
                .Where(c=>c.LecturerId== userId&&c.DateTime>DateTime.Now)
                .Include(l=>l.Lecturer)
                .Include (c=>c.Category)
                .ToList();
            return View(course);
        }
    }
}
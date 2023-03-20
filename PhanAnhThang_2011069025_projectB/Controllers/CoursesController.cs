using Microsoft.AspNet.Identity;
using PhanAnhThang_2011069025_projectB.Models;
using PhanAnhThang_2011069025_projectB.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhanAnhThang_2011069025_projectB.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]//ktr notice ma cho user khop vs ma ban dau hay kh -0:er400
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var sourse = new Course
            {
                LecturerId = User.Identity.GetUserId(),//get ID User using, return chuoi_string 
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Course.Add(sourse);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");//chuyen huong den Index thuoc /Home/Index
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();
            var viewModel = new CoursesViewModel
            {
                UpcommingCourses=courses,
                ShowAction=User.Identity.IsAuthenticated,
            };
            return View(viewModel);
        }
    }
}
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
                Categories = _dbContext.Categories.ToList(),
                Heading="Add Course"
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
            return RedirectToAction("Mine", "Courses");//chuyen huong den Index thuoc /Home/Index
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

        [Authorize]
        public ActionResult Mine()
        {
            var userId=User.Identity.GetUserId();
            var courses = _dbContext.Course
                .Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now)
                .Include(l => l.Lecturer)
                .Include(c => c.Category)
                .ToList();
            return View(courses);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId=User.Identity.GetUserId();
            var courses = _dbContext.Course
                .Single(c => c.ID == id && c.LecturerId == userId);
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Date = courses.DateTime.ToString("dd/M/yyyy"),
                Time = courses.DateTime.ToString("HH:mm"),
                Category = courses.CategoryId,
                Place = courses.Place,
                Heading="Edit Course",
                Id=courses.ID
            };
            return View("Create",viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                viewModel.Categories=_dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var userId=User.Identity.GetUserId();
            var courses = _dbContext.Course
                .Single(c=>c.ID==viewModel.Id&&c.LecturerId==userId);
            courses.Place=viewModel.Place;
            courses.DateTime=viewModel.GetDateTime();
            courses.CategoryId = viewModel.Category;
            _dbContext.SaveChanges();
            return RedirectToAction("Mine", "Courses");
        }
    }
}
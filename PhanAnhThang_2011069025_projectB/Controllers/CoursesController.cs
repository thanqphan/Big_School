using Microsoft.AspNet.Identity;
using PhanAnhThang_2011069025_projectB.Models;
using PhanAnhThang_2011069025_projectB.ViewModels;
using System;
using System.Collections.Generic;
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
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories=_dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var sourse = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Course.Add(sourse);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
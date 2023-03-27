using Microsoft.AspNet.Identity;
using PhanAnhThang_2011069025_projectB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhanAnhThang_2011069025_projectB.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _dbContext { get; set; }
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var course=_dbContext.Course.Single(c=>c.ID==id&&c.LecturerId==userId);
            if(course.IsCanceled)
            {
                return NotFound();
            }
            course.IsCanceled= true;
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}

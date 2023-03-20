using Microsoft.AspNet.Identity;
using PhanAnhThang_2011069025_projectB.DTOs;
using PhanAnhThang_2011069025_projectB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhanAnhThang_2011069025_projectB.Controllers
{
    [Authorize]
    public class AttendanceController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendanceController()
        {
            _dbContext= new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
            {
                return BadRequest("The Attendance already exists!");
            }
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId,
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
        /*public IHttpActionResult Attend([FromBody] int courseId)
        {
            var userId=User.Identity.GetUserId();
            if(_dbContext.Attendances.Any(a=>a.AttendeeId==userId && a.CourseId==courseId)) {
                return BadRequest("The Attendane already exists!");
            }
            var attendance = new Attendance
            {
                CourseId = courseId,
                AttendeeId=userId,
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }*/
    }
}

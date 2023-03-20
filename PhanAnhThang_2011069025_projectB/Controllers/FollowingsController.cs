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
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();
            if(_dbContext.Followings.Any(f=>f.FollowerId==userId&&f.FollowerId==followingDto.FolloweeId)) {
                return BadRequest("Following already exists!");
            }
            var folowing = new Following
            {
                FollowerId=userId,
                FolloweeId=followingDto.FolloweeId,
            };
            _dbContext.Followings.Add(folowing);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}

using System;
using German.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace German.Persistence
{
	public partial class AppDbContext
	{

        public DbSet<UserCourse> UserCourses { get; set; }



        public async Task<UserCourse> CreateUserCourseAsync(UserCourse userCourse)
        {
            //check entityentry class again
            EntityEntry<UserCourse> courseEntry = await this.UserCourses.AddAsync(userCourse);
            await this.SaveChangesAsync();
            return courseEntry.Entity;
        }

        public async Task<List<UserCourse>> SelectAllUserCourseAsync(int userId)
        {
            return await this.UserCourses.Where(uc => uc.UserId == userId).ToListAsync();
        }

     



    }
}


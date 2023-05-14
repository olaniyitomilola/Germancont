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
            EntityEntry<UserCourse> courseEntry = await this.UserCourses.AddAsync(userCourse);
            await this.SaveChangesAsync();
            return courseEntry.Entity;
        }
        public async Task<List<UserCourse>> SelectAllUserCourseByIdAsync(int userId)
        {
            return await UserCourses.Where(uc => uc.UserId == userId)
                .Include(c=>c.course).ToListAsync();
        }





    }
}


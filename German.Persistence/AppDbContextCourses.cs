using System;
using German.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace German.Persistence
{
	public partial class AppDbContext
	{

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLesson> CourseLessons { get; set; } 



        public async Task<Course> CreateCourseAsync(Course course)
        {
            //check entityentry class again
            EntityEntry<Course> courseEntry = await this.Courses.AddAsync(course);
            await this.SaveChangesAsync();
            return courseEntry.Entity;
        }

        public async Task<Course> DeleteCourseAsync(Course course)
        {
            EntityEntry<Course> entityEntry = this.
                Courses.Remove(course);
            await this.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<List<Course>> SelectAllCoursesAsync()
        {
            return await this.Courses.ToListAsync();
        }

        public async Task<Course> SelectCourseByIdAsync(int courseId)
        {
            var course = await this.Courses
                .AsNoTracking()
                .Include(c => c.CourseLessons) //the Icollection in the courseObject //if you end up having attackment
                    //.ThenInclude(c=> c.Authors) //changed to single single author per course
                //as different entity, implement here too
                .FirstOrDefaultAsync(course => course.Id == courseId);

            if (course is null) throw new NullReferenceException($"no course with id {courseId}");

            return course;
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            EntityEntry<Course> entityEntry = this.Courses.Update(course);
            await this.SaveChangesAsync();
            return entityEntry.Entity;
        }




    }
}


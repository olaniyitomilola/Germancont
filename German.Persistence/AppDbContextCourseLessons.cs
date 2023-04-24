using System;
using German.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace German.Persistence
{
	public partial class AppDbContext
	{

        public DbSet<CourseLesson> CourseLessons { get; set; } 



        public async Task<CourseLesson> CreateCourseLessonAsync(CourseLesson lesson)
        {
            //check entityentry class again
            EntityEntry<CourseLesson> courseEntry = await this.CourseLessons.AddAsync(lesson);
            await this.SaveChangesAsync();
            return courseEntry.Entity;
        }

        public async Task<CourseLesson> DeleteCourseLessonAsync(int lessonid)
        {
            try {

                CourseLesson lesson = await this.SelectCourseLessonByIdAsync(lessonid);
                EntityEntry<CourseLesson> entityEntry = this.
                    CourseLessons.Remove(lesson);
                    await this.SaveChangesAsync();
                    return entityEntry.Entity;
            }
            catch(Exception ex)
            {
                if(ex is ApplicationException)
                {
                    throw new ApplicationException($"Course lesson with id {lessonid} does not exist");
                }
              
                    throw new Exception(ex.Message);
                
  
            }
            
        }

        public async Task<List<CourseLesson>> SelectAllCourseLessonsAsync()
        {
            return await this.CourseLessons.ToListAsync();
        }

        public async Task<CourseLesson> SelectCourseLessonByIdAsync(int CourseLessonId)
        {
            var CourseLesson = await this.CourseLessons
                .AsNoTracking()
               // .Include(c => c.CourseLessonLessons) //the Icollection in the CourseLessonObject //if you end up having attackment
                    //.ThenInclude(c=> c.Authors) //changed to single single author per CourseLesson
                //as different entity, implement here too
                .FirstOrDefaultAsync(CourseLesson => CourseLesson.Id == CourseLessonId);

            if (CourseLesson is null) throw new ApplicationException($"no CourseLesson with id {CourseLessonId}");

            return CourseLesson;
        }

        public async Task<CourseLesson> UpdateCourseLessonAsync(CourseLesson CourseLesson)
        {
            EntityEntry<CourseLesson> entityEntry = this.CourseLessons.Update(CourseLesson);
            await this.SaveChangesAsync();
            return entityEntry.Entity;
        }




    }
}


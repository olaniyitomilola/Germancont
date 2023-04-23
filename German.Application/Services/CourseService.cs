using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using German.Core.Entities;
using German.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace German.Application.Services
{

    public class CourseService : ICourseService
    {
        private readonly IAppDbContext db;
        private readonly ILogger<CourseService> logger;

        public CourseService(ILogger<CourseService> logger, IAppDbContext db) { 
            this.db = db;
            this.logger = logger;
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            return await db.SelectAllCoursesAsync();
        }
        public async Task<Course> GetCourseAsync(int Id)
        {
            var course = await  db.SelectCourseByIdAsync(Id);

            if(course is null) { throw new ApplicationException($"Course with Id {Id} not found"); }
            return course;
        }
    }
}

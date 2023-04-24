using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using German.Core.Entities;

namespace German.Core.Interfaces
{
	public partial interface IAppDbContext
	{
        //had to remove the public declarations here becauyse all members are public natuyrally and adding it will trigger an error
		 Task<CourseLesson> CreateCourseLessonAsync(CourseLesson lesson);
         Task<CourseLesson> SelectCourseLessonByIdAsync(int  courselessonId);
         Task<List<CourseLesson>> SelectAllCourseLessonsAsync();
         Task<CourseLesson> UpdateCourseLessonAsync(CourseLesson lesson);
         Task<CourseLesson> DeleteCourseLessonAsync(int lessonid);



    }
}


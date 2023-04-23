using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using German.Core.Entities;

namespace German.Core.Interfaces
{
	public partial interface IAppDbContext
	{
        //had to remove the public declarations here becauyse all members are public natuyrally and adding it will trigger an error
		 Task<Course> CreateCourseAsync(Course course);
         Task<Course> SelectCourseByIdAsync(int  courseId);
         Task<List<Course>> SelectAllCoursesAsync();
         Task<Course> UpdateCourseAsync(Course course);
         Task<Course> DeleteCourseAsync(Course course);



    }
}


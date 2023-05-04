using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using German.Core.Entities;

namespace German.Core.Interfaces
{
	public partial interface IAppDbContext
	{
		 Task<UserCourse> CreateUserCourseAsync(UserCourse userCourse);
         Task<List<UserCourse>> SelectAllUserCourseByIdAsync(int  userId);
   



    }
}


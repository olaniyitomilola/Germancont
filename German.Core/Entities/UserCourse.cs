using System;
using System.Collections.Generic;
using System.Text;

namespace German.Core.Entities
{
    public class UserCourse
    {
        public int UserId { get; set; }
       public User user { get; set; }
        public int CourseId { get; set; }
        public Course course { get; set; }

    }
}

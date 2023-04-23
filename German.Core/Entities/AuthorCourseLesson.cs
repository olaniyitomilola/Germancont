using System;
using System.Collections.Generic;
using System.Text;

namespace German.Core.Entities
{
    public class AuthorCourseLesson
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int CourseLessonId { get; set; }
        public CourseLesson courseLessons { get; set; }

    }
}

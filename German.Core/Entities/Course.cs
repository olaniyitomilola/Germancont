using System;
using System.Collections.Generic;
using German.Core.Interfaces;
namespace German.Core.Entities
{
	public class Course : IAuditableEntity
	{
		public Course()
		{
            this.CourseLessons = new HashSet<CourseLesson>();
            this.authors = new HashSet<UserCourse>();
		}

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseUrl { get; set; }
        public Author author { get; set; }
        public int authorid { get; set; }

        public ICollection<CourseLesson> CourseLessons { get;set; }
        public ICollection<UserCourse> authors{ get; set; }


    }
}


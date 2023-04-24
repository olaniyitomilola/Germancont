using System;
using German.Core.Entities;

namespace German.Core.DTOs
{
	public class CourseDto
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string CourseUrl { get; set; }
        public int authorid { get; set; }
    }
}


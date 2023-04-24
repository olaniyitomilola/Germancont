using System;
using System.ComponentModel.DataAnnotations;
using German.Core.Entities;

namespace German.Core.DTOs
{
	public class CourseLessonDto

	{
        public int CourseId { get; set; }

        public string LessonTitle { get; set; }
     
        public string LessonParagraphs { get; set; }
    }
}


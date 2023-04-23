using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using German.Core.Entities;
using German.Core.Interfaces;
using German.Core.Types;

namespace German.Core.Entities
{
    public class CourseLesson : IAuditableEntity
    {
        public CourseLesson() { 
        }
        public int Id { get; set ; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; }

        //navigation property to parent course
        public Course course { get; set; }  
        public int CourseId { get; set; }

        [Required]
        public string LessonName { get; set; }
        [Required]
        public string LessonDescription { get;set;}
        [Required]
        public SubscriptionType SubscriptionType { get; set; }

        public String Attachments { get; set; }




    }
}

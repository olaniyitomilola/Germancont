using System;
using System.Collections.Generic;
using System.Text;
using German.Core.Interfaces;

namespace German.Core.Entities
{
    public class UserCourse : IAuditableEntity
    {
        public int UserId { get; set; }
        public Author user { get; set; }
        public int CourseId { get; set; }
        public Course course { get; set; }
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
    }
}

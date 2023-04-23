using System;
using System.Collections.Generic;
using System.Text;
using German.Core.Interfaces;
namespace German.Core.Entities
{
    public class Author : IAuditableEntity
    {
        public Author() { 
           // this.CourseLessons = new HashSet<AuthorCourseLesson>(); //made one author per course
            this.Courses = new HashSet<Course>();
        }
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        //core identity uses email as primary username
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Suffix { get; set; }
        public string webUrl { get; set; }
        public string Description { get; set; }
       


        public ICollection<AuthorCourseLesson> CourseLessons { get;set; }
        public ICollection<Course> Courses { get; set; }
    }
}

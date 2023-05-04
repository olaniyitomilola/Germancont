using System;
namespace German.Core.DTOs
{
	public class AuthorProfileDto
	{
        //to send out profile withoput the password
		public AuthorProfileDto()
		{

		}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        //core identity uses email as primary username
        public string PhoneNumber { get; set; }
        public string webUrl { get; set; }
        public string Description { get; set; }
        public bool Contributor { get; set; }
    }
}


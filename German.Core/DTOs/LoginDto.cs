using System;
using System.ComponentModel.DataAnnotations;
using German.Core.Entities;

namespace German.Core.DTOs
{
	public class LoginDto

	{

        public string email { get; set; }
     
        public string password { get; set; }
    }
}


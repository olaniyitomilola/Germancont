﻿using System;
namespace German.Core.DTOs
{
    public class Authordto
    {
        public Authordto()
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        //core identity uses email as primary username
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string webUrl { get; set; }
        public string Description { get; set; }
    }
}


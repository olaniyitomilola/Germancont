using System;
using German.Core.Interfaces;
using System.Threading.Tasks;
using German.Core.Entities;
using Microsoft.AspNetCore.Identity;
using German.Core.DTOs;

namespace German.Application.Services
{
	public class AuthorAuthService : IAuthorAuthService
	{
        private readonly IAppDbContext _db;
		public AuthorAuthService(IAppDbContext db)
		{
            _db = db;


		}
        private PasswordHasher<Author> _passwordHasher = new PasswordHasher<Author>();

        public async Task<Author> Authenticate(LoginDto loginDto)
        {
            
            var author = await _db.SelectAuthorByEmailAsync(loginDto.email);
            //check password and hash password
            if (author == null || _passwordHasher.VerifyHashedPassword(author, author.Password, loginDto.password) != PasswordVerificationResult.Success)
            {
                return null;
            }

            return author;
        }
    }
}


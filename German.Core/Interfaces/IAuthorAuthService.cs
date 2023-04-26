using System;
using System.Threading.Tasks;
using German.Core.Entities;
using German.Core.DTOs;

namespace German.Core.Interfaces
{
	public interface IAuthorAuthService
	{
		Task<Author> Authenticate(LoginDto logindto);
		Task<bool> EmailExists(string email);
	}
}


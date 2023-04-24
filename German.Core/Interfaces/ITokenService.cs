using System;
using German.Core.Entities;

namespace German.Core.Interfaces
{
	public interface ITokenService
	{
        string GenerateToken(Author user);

    }
}


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using German.Core.Entities;

namespace German.Core.Interfaces
{
	public partial interface IAppDbContext
	{
        //had to remove the public declarations here becauyse all members are public natuyrally and adding it will trigger an error
		 Task<Author> CreateAuthorAsync(Author author);
         Task<Author> SelectAuthorByIdAsync(int  authorId);
         Task<List<Author>> SelectAllAuthorsAsync();
         Task<Author> UpdateAuthorAsync(Author author);
         Task<Author> DeleteAuthorAsync(Author author);



    }
}


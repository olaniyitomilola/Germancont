using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using German.Core.Entities;


namespace German.Core.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAuthorsAsync();
        Task<Author> AuthorGetByIdAsync(int id);
        Task<Author> AddAuthorAsync(Author author);
        Task<Author> UpdateAuthorAsync(Author author, int id);
        Task<Author> DeleteAuthorAsync(int id);
    }
}

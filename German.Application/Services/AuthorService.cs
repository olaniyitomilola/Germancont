//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using German.Core.Entities;
//using German.Core.Interfaces;
//using Microsoft.Extensions.Logging;

//namespace German.Application.Services
//{
//    public  class AuthorService : IAuthorService
//    {
//        private readonly IAppDbContext _db;
//        private ILogger<AuthorService> _logger;

//        public AuthorService(IAppDbContext db, ILogger<AuthorService> logger)
//        {
//            _db = db;
//            _logger = logger;
//        }

//        public Task<Author> AddAuthorAsync(Author author)
//        {
            
//        }

//        public Task<Author> AuthorGetByIdAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Author> DeleteAuthorAsync(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<List<Author>> GetAuthorsAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Author> UpdateAuthorAsync(Author author, int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

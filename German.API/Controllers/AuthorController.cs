using Microsoft.AspNetCore.Mvc;
using German.Persistence;
using German.Core.Interfaces;
using German.Core.Entities;
using Microsoft.AspNetCore.Identity;
using German.Application.DTOs;

namespace German.API.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAppDbContext _db;
        private readonly ILogger<AuthorController> _logger;
        public AuthorController(IAppDbContext db, ILogger<AuthorController> logger) {
            _db = db;
            _logger = logger;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var authors = await _db.SelectAllAuthorsAsync();

                return Ok(authors);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex); 
                return BadRequest(ex.Message);  
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var authors = await _db.SelectAuthorByIdAsync(id);
                return Ok(authors);


            } catch(Exception ex) { 

                if(ex is ApplicationException)
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);

                }
            }

        }

        [HttpPost]

        public async Task<IActionResult> Post(Authordto authordto)
        {

            Author author = new Author();
            author.FirstName = authordto.FirstName;
            author.LastName = authordto.LastName;
            author.MiddleName = authordto.MiddleName;
            author.Password = authordto.Password;
            author.PhoneNumber = authordto.PhoneNumber;
            author.Suffix = author.Suffix;
            author.Email = authordto.Email;
            author.Description = authordto.Description;
            author.webUrl = authordto.webUrl;

            var passwordHasher = new PasswordHasher<Author>();


            //test
            author.Password = passwordHasher.HashPassword(author, author.Password);
            var response = await _db.CreateAuthorAsync(author);


            /*

            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, hashedPassword, plainTextPassword);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                // Password is correct
            }
            else
            {
                // Password is incorrect
            }

            */

            return CreatedAtAction(nameof(GetById), new { id = author.Id}, author);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var author = await _db.SelectAuthorByIdAsync(id);

                try
                {
                   var response = await _db.DeleteAuthorAsync(author);
                   return Ok(response);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);
                }
                
            } catch(Exception ex)
            {
                if (ex is ApplicationException)
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);

                }
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, Authordto newAuthor)
        {
            try
            {
                var author = await _db.SelectAuthorByIdAsync(id);

                try
                {
                    if (newAuthor.FirstName != null) author.FirstName = newAuthor.FirstName;
                    if (newAuthor.LastName != null) author.LastName = newAuthor.LastName;
                    if (newAuthor.webUrl != null) author.webUrl = newAuthor.webUrl;
                    if (newAuthor.PhoneNumber != null) author.PhoneNumber = newAuthor.PhoneNumber;
                    if (newAuthor.Email != null) author.Email = newAuthor.Email;
                    if (newAuthor.Description != null) author.Description = newAuthor.Description;
                    if (newAuthor.Suffix != null) author.Suffix = newAuthor.Suffix;

                    var response = await _db.UpdateAuthorAsync(author);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);
                }

            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);

                }
            }

        }
    }
}

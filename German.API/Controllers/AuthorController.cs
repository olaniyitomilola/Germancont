using Microsoft.AspNetCore.Mvc;
using German.Persistence;
using German.Core.Interfaces;
using German.Core.Entities;
using Microsoft.AspNetCore.Identity;
using German.Core.DTOs;
using AutoMapper;
using NuGet.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace German.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAppDbContext _db;
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorAuthService _authorAuthService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AuthorController(IMapper mapper, IAppDbContext db, ILogger<AuthorController> logger,IAuthorAuthService authorAuthService, ITokenService tokenService) {
            _db = db;
            _logger = logger;
            _authorAuthService = authorAuthService;
            _tokenService = tokenService;
            _mapper = mapper;
        }
     

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var authors = await _db.SelectAllAuthorsAsync();

                var authorsprofile = _mapper.Map<List<AuthorProfileDto>>(authors);


                return Ok(authorsprofile);
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
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            try
            {
                //gets the Sub value
                var userId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Unable to retrieve ser info");
                }
                if(int.TryParse(userId, out int id)){

                    Author author = await _db.SelectAuthorByIdAsync(id);

                    if(author != null)
                    {
                        return Ok(author);
                    }
                    return NotFound();


                }else
                {
                    return Unauthorized();
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

        [Authorize]
        [HttpPut("becomeacontributor")]
        public async Task<IActionResult> BecomeAContributor()
        {
            try
            {
                //gets the Sub value
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Unable to retrieve ser info");
                }
                if (int.TryParse(userId, out int id))
                {

                    Author author = await _db.SelectAuthorByIdAsync(id);

                    if (author != null)
                    {
                        try
                        {
                            author.Contributor = true;
                            var newAuthor = await _db.UpdateAuthorAsync(author);

                            return Ok(newAuthor);

                        }catch(Exception ex)
                        {
                            _logger.LogError(ex.Message);
                            return BadRequest(ex.Message);
                        }
                    }
                    return NotFound();


                }
                else
                {
                    return Unauthorized();
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



        [Authorize]
        [HttpGet("courses")]
        public async Task<IActionResult> GetUserCourse()
        {
            try
            {
                //gets the Sub value
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Unable to retrieve ser info");
                }
                if (int.TryParse(userId, out int id))
                {

                    Author author = await _db.SelectAuthorByIdAsync(id);

                    if (author != null)
                    {
                        return Ok(author.myCourses);
                    }
                    return NotFound();


                }
                else
                {
                    return Unauthorized();
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

        [Authorize]
        [HttpGet("contributor/courses")]
        public async Task<IActionResult> GetContributorCourse()
        {
            try
            {
                //gets the Sub value
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Unable to retrieve ser info");
                }
                if (int.TryParse(userId, out int id))
                {

                    Author author = await _db.SelectAuthorByIdAsync(id);

                    if (author != null)
                    {
                        if (author.Contributor == false) return Unauthorized();
                        return Ok(author.Courses);
                    }
                    return NotFound();


                }
                else
                {
                    return Unauthorized();
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


        [HttpPost("login")]
        public async Task<IActionResult> Login( [FromBody] LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var author = await _authorAuthService.Authenticate(loginDto);

                if(author == null)
                {
                    return Unauthorized();
                }

                var token = _tokenService.GenerateToken(author);

                return Ok(new { token });
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
                    return BadRequest(ex.ToString());

                }
            }

        }

        [HttpPost("signup")]

        public async Task<IActionResult> Signup([FromBody]Authordto authordto)
        {

            try
            {
                
                bool exist = await _authorAuthService.EmailExists(authordto.Email);

                if (exist)
                {
                    return Conflict("Email is already in use");
                }

                //use AutoMapper for this later
                Author autho = _mapper.Map<Author>(authordto);
                //Author author = new Author();
                //author.FirstName = authordto.FirstName;
                //author.LastName = authordto.LastName;
                //author.MiddleName = authordto.MiddleName;
                //author.Password = authordto.Password;
                //author.PhoneNumber = authordto.PhoneNumber;
                //author.Email = authordto.Email;
                //author.Description = authordto.Description;
                //author.webUrl = authordto.webUrl;

                var passwordHasher = new PasswordHasher<Author>();


                //test
                autho.Password = passwordHasher.HashPassword(autho, autho.Password);
                var response = await _db.CreateAuthorAsync(autho);

                var token = _tokenService.GenerateToken(response);

                return Ok(new { token });
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
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
                    if (newAuthor.Password != null) author.Password = newAuthor.Password;

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

using Microsoft.AspNetCore.Mvc;
using German.Persistence;
using German.Core.Interfaces;
using German.Core.Entities;
using German.Core.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace German.API.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly IAppDbContext _db;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;
        public CourseController(IMapper mapper, IAppDbContext db, ILogger<AuthorController> logger)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            try
            {
                var courses = await _db.SelectAllCoursesAsync();

                return Ok(courses);
            }
            catch (Exception ex)
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
                var course = await _db.SelectCourseByIdAsync(id);
                
                return Ok(course);


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
        [HttpPost("enrol/{courseId}")]
        public async Task<IActionResult> Enrol(int courseId)
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
                            Course course = await _db.SelectCourseByIdAsync(courseId);
                            if(course != null)
                            {
                                UserCourse usercourse = new UserCourse()
                                {
                                    //course = course,
                                    //user = author,
                                    UserId = id,
                                    CourseId = courseId

                                };

                                /*
                                 //ERROR
                                   Cannot insert explicit value for identity column in table 'Authors' when IDENTITY_INSERT is set to OFF.
                                    Cannot insert explicit value for identity column in table 'Courses' when IDENTITY_INSERT is set to OFF.
                                    The INSERT statement conflicted with the FOREIGN KEY constraint "FK_UserCourses_users_UserId". The conflict occurred in database "GermanLMS", table "dbo.users", column 'Id'.
                                    The statement has been terminated.
                                 
                                 */
                                //temporaty manouver using update.

                                var response = await _db.CreateUserCourseAsync(usercourse);

                                return Ok(response);



                                //author.myCourses.Add(new UserCourse()
                                //{
                                //    course = course,
                                //    CourseId = course.Id,
                                //    UserId = author.Id
                                //});

                                //var response = _db.UpdateAuthorAsync(author);

                                //return Ok(author.myCourses);

                            }
                            else
                            {
                                return BadRequest("Course does not exist");
                            }
                        }
                        catch(Exception ex)
                        {
                            _logger.LogError(ex.Message);
                        }
                    }
                    return NotFound();


                }
                else
                {
                    _logger.LogInformation($"Unauthorised access");
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


        [HttpPost]

        //authorize that the poster is a contributor

        public async Task<IActionResult> Post(CourseDto coursedto)
        {

            Course course = new Course();
            course.Title = coursedto.Title;
            course.Description = coursedto.Description;
            course.authorid = coursedto.authorid;

            try
            {
               //var author = await _db.SelectAuthorByIdAsync(coursedto.authorid);
               //var authorsprofile = _mapper.Map<AuthorProfileDto>(author);

               // course.author = _mapper.Map<Author>(authorsprofile);
                var response = await _db.CreateCourseAsync(course);


                return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                if (ex is ApplicationException)
                {
                    return NotFound("Author does not exist");
                }
                else {
                    return BadRequest(ex.Message);
                }

            }
        
        }
        //do authorization
        //just check the id of the user
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var course = await _db.SelectCourseByIdAsync(id);

                try
                {
                    var response = await _db.DeleteCourseAsync(course);
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

        [HttpPut("{id}")]

        public async Task<IActionResult> Put(int id, CourseDto course)
        {
            try
            {
                var excourse = await _db.SelectCourseByIdAsync(id);

                excourse.Title = course.Title;
                excourse.Description = course.Description;
                excourse.CourseUrl = course.CourseUrl;
                if (course.authorid != excourse.authorid) excourse.authorid = course.authorid;
                try
                {
                    var response = await _db.UpdateCourseAsync(excourse);
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

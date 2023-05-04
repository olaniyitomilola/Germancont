using Microsoft.AspNetCore.Mvc;
using German.Persistence;
using German.Core.Interfaces;
using German.Core.Entities;
using German.Core.DTOs;
using AutoMapper;

namespace German.API.Controllers
{
    [Route("api/[controller]")]
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
               var author = await _db.SelectAuthorByIdAsync(coursedto.authorid);
               var authorsprofile = _mapper.Map<AuthorProfileDto>(author);

                course.author = _mapper.Map<Author>(authorsprofile);
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
                excourse.CourseUrl = course.Description;
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

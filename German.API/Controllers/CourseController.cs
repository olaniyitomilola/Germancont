using Microsoft.AspNetCore.Mvc;
using German.Persistence;
using German.Core.Interfaces;
using German.Core.Entities;

namespace German.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly IAppDbContext _db;
        private readonly ILogger<AuthorController> _logger;
        public CourseController(IAppDbContext db, ILogger<AuthorController> logger)
        {
            _db = db;
            _logger = logger;
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

        public async Task<IActionResult> Post(Course course)
        {
            var response = await _db.CreateCourseAsync(course);

            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }
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

        public async Task<IActionResult> Put(int id, Course course)
        {
            try
            {
                var excourse = await _db.SelectCourseByIdAsync(id);

                excourse.Name = course.Name;
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

using German.Core.DTOs;
using German.Core.Entities;
using German.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace German.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseLessonController : Controller
    {

        private readonly IAppDbContext _db;
        private readonly ILogger<CourseLessonController> _logger;
        public CourseLessonController(IAppDbContext db, ILogger<CourseLessonController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int lessonid)
        {
            try
            {
                var course = await _db.SelectCourseLessonByIdAsync(lessonid);

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
        public async Task<IActionResult> Post(CourseLessonDto courseLessondto)
        {

            CourseLesson course = new CourseLesson();
            course.LessonTitle = courseLessondto.LessonTitle;
            course.CourseId = courseLessondto.CourseId;
            course.LessonParagraphs = courseLessondto.LessonParagraphs;               
        

            try
            {
                var myCourse = await _db.SelectCourseByIdAsync(courseLessondto.CourseId);
               // course.course = myCourse;
                var response = await _db.CreateCourseLessonAsync(course);

                return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (ex is ApplicationException)
                {
                    return NotFound("Course does not exist");
                }
                else
                {
                    return BadRequest(ex.ToString());
                }

            }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Context;
using Server.Models.Entities;
using Server.Models.Pagination;
using Server.Repository.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly AppDbContext _context;
        
        public CoursesController(IUnitOfWork uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {

            return await _uow.CourseRepository.Get().ToListAsync();
        }
       
        
        // GET: api/Courses/Diciplines
        [HttpGet("Diciplines/{id}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesDiciplines(int id)
        {
            var courses = await _uow.CourseRepository.GetCourseWithDiciplines(id);

            return Ok(courses);
        }        

        // GET: api/Course/Paged
        [HttpGet("Paged")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesPaged([FromQuery] PaginationParammeters paginationParammeters)
        {

            PagedList<Course> courses = await _uow.CourseRepository.GetPaged(
                paginationParammeters,
                _uow.CourseRepository.Get()
                );

            var metadata = new
            {
                courses.TotalCount,
                courses.PageSize,
                courses.CurrentPage,
                courses.TotalPages,
                courses.HasNext,
                courses.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {

            var course = await _uow.CourseRepository.GetById(c => c.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }


        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {

            _uow.CourseRepository.Add(course);

            try
            {
                await _uow.Commit();
            }
            catch (DbUpdateException)
            {

                //return BadRequest("This course alredy exists");
                throw;
            }

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            if (!await CourseExists(id))
            {
                return BadRequest("Course not founded");

            }

            _uow.CourseRepository.Upadate(course);

            try
            {
                await _uow.Commit();
            }
            catch (DbUpdateException)
            {
                return BadRequest("This course alredy exist in database");
            }

            return NoContent();
        }
        
        /*
        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Diciplines/{id}")]
        public async Task<IActionResult> PutCourseDiciplines(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            if (!await CourseExists(id))
            {
                return BadRequest("Course not founded");

            }

            foreach (var dicipline in course.Diciplines)
            {
                var courseDicipline = new CourseDicipline
                {
                    CoursesCourseId = id,
                    DiciplinesDiciplineId = dicipline.DiciplineId
                };
                _context.CourseDicipline.Add(courseDicipline);

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("This course alredy exist");
            }

            return NoContent();
        }
        */


        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {

            var course = await _uow.CourseRepository.GetById(c => c.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            _uow.CourseRepository.Delete(course);
            await _uow.Commit();

            return NoContent();
        }

        private async Task<bool> CourseExists(int id)
        {
            var course = await _uow.CourseRepository.GetById(c => c.CourseId == id);
            if (course == null)
            {
                return false;
            }
            return true;
        }
    }
}

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
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _uow;

        public StudentsController(IUnitOfWork uow, AppDbContext context)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _uow.StudentsRepository.Get().ToListAsync();
        
        }
        // GET: api/Students
        [HttpGet("Paged")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsPaged([FromQuery] PaginationParammeters paginationParammeters)
        {
            var students =  await _uow.StudentsRepository.GetPaged(
                paginationParammeters,
                _uow.StudentsRepository.Get()
               .Include(s => s.Course)
                );

            var metadata = new
            {
                students.TotalCount,
                students.PageSize,
                students.CurrentPage,
                students.TotalPages,
                students.HasNext,
                students.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return students;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {

            var student = await _uow.StudentsRepository.GetById(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }   

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }
            if (! await StudentExists(id))
            {
                return NotFound();
            }
            _uow.StudentsRepository.Upadate(student);

            try
            {
                await _uow.Commit();
            }
            catch (DbUpdateException)
            {
                _uow.Dispose();
                return BadRequest("This student alredy exists");
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
          if (_context.Students == null)
          {
              return Problem("Entity set 'AppDbContext.Students'  is null.");
          }
            _uow.StudentsRepository.Add(student);
            try
            {
            await _uow.Commit();

            }
            catch (DbUpdateException)
            {
                _uow.Dispose();
                return BadRequest("This student alredy exists");
            }

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {

            var student = await _uow.StudentsRepository.GetById(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            _uow.StudentsRepository.Delete(student);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> StudentExists(int id)
        {
            var e = await _uow.StudentsRepository.GetById(c => c.StudentId == id);
            if (e == null)
            {
                return false;
            }
            return true;
        }
    }
}

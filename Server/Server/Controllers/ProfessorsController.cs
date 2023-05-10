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
    public class ProfessorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _uow;

        public ProfessorsController(IUnitOfWork uow, AppDbContext context)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Professors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessors()
        {
    
            return await _uow.ProfessorsRepository.Get().ToListAsync();
        }

        // GET: api/Professors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetProfessor(int id)
        {

            var professor = await _uow.ProfessorsRepository.GetById(p => p.ProfessorId == id);

            if (professor == null)
            {
                return NotFound();
            }

            return professor;
        }

        // GET: api/Professor/Paged
        [HttpGet("Paged")]
        public async Task<ActionResult<IEnumerable<Professor>>> GetProfessorsPaged([FromQuery] PaginationParammeters paginationParammeters)
        {

            var professors = await _uow.ProfessorsRepository.GetPaged(
                paginationParammeters,
                _uow.ProfessorsRepository.Get()
                );

            var metadata = new
            {
                professors.TotalCount,
                professors.PageSize,
                professors.CurrentPage,
                professors.TotalPages,
                professors.HasNext,
                professors.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return professors;
        }

        // PUT: api/Professors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(int id, Professor professor)
        {
            if (id != professor.ProfessorId)
            {
                return BadRequest();
            }
            if (!await ProfessorExists(id))
            {
                return NotFound();
            }
            _uow.ProfessorsRepository.Upadate(professor);

            try
            {
                await _uow.Commit();
            }
            catch (DbUpdateException)
            {
                _uow.Dispose();
                return BadRequest("This professor alredy exists");
            }

            return NoContent();
        }

        // POST: api/Professors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Professor>> PostProfessor(Professor professor)
        {
          if (_context.Professors == null)
          {
              return Problem("Entity set 'AppDbContext.Professors'  is null.");
          }
            _uow.ProfessorsRepository.Add(professor);

            try
            {
                await _uow.Commit();

            }
            catch (DbUpdateException)
            {

                _uow.Dispose();
                return BadRequest("This professor alredy exists");
            }

            return CreatedAtAction("GetProfessor", new { id = professor.ProfessorId }, professor);
        }

        // DELETE: api/Professors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var professor = await _uow.ProfessorsRepository.GetById(p => p.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            _uow.ProfessorsRepository.Delete(professor);
            await _uow.Commit();

            return NoContent();
        }

        private async Task<bool> ProfessorExists(int id)
        {
            var e = await _uow.ProfessorsRepository.GetById(c => c.ProfessorId == id);
            if (e == null)
            {
                return false;
            }
            return true;
        }
    }
}

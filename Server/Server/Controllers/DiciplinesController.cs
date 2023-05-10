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
    public class DiciplinesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public DiciplinesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Diciplines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dicipline>>> GetDiciplines()
        {

            return await _uow.DiciplineRepository.Get().ToListAsync();
        }

        // GET: api/Diciplines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dicipline>> GetDicipline(int id)
        {
            var dicipline = await _uow.DiciplineRepository.GetById(d => d.DiciplineId == id);

            if (dicipline == null)
            {
                return NotFound();
            }

            return dicipline;
        }
        // GET: api/Diciplines/Paged
        [HttpGet("Paged")]
        public async Task<ActionResult<IEnumerable<Dicipline>>> GetDiciplinesPaged([FromQuery] PaginationParammeters paginationParammeters)
        {

            var diciplines = await _uow.DiciplineRepository.GetPaged(
                paginationParammeters,
                _uow.DiciplineRepository.Get()
                );

            var metadata = new
            {
                diciplines.TotalCount,
                diciplines.PageSize,
                diciplines.CurrentPage,
                diciplines.TotalPages,
                diciplines.HasNext,
                diciplines.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return diciplines;
        }

        // POST: api/Diciplines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dicipline>> PostDicipline(Dicipline dicipline)
        {

            _uow.DiciplineRepository.Add(dicipline);
            try
            {
                await _uow.Commit();
            }
            catch (DbUpdateException)
            {
                _uow.Dispose();
                return BadRequest("This dicipline alredy exist");
            }

            return CreatedAtAction("GetDicipline", new { id = dicipline.DiciplineId }, dicipline);
        }


        // PUT: api/Diciplines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDicipline(int id, Dicipline dicipline)
        {
            if (id != dicipline.DiciplineId)
            {
                return BadRequest("Id mismacht");
            }

            if (!await DiciplineExists(id))
            {
                return BadRequest("This dicipline dont exist");
            }
            _uow.DiciplineRepository.Upadate(dicipline);

            try
            {
                await _uow.Commit();
            }
            catch (DbUpdateException)
            {
                _uow.Dispose();
                return BadRequest("This dicipline alredy exists in database");
            }

            return NoContent();
        }



        // DELETE: api/Diciplines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDicipline(int id)
        {

            var dicipline = await _uow.DiciplineRepository.GetById(d => d.DiciplineId == id);
            if (dicipline == null)
            {
                return NotFound();
            }

            _uow.DiciplineRepository.Delete(dicipline);
            _uow.Commit();

            return NoContent();
        }

        private async Task<bool> DiciplineExists(int id)
        {
            var dicipline = await _uow.DiciplineRepository.GetById(c => c.DiciplineId == id);
            if (dicipline == null)
            {
                return false;
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullStackWebApp.DbContexts;
using FullStackWebApp.Models;
using System.Net.Http;
using AutoMapper;

namespace FullStackWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AanbodController : ControllerBase
    {
        private readonly MainSqlServerDbContext _context;
        private FundaService _service;

        public AanbodController(MainSqlServerDbContext context, FundaService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Aanbod/GetAanbodFromUrl
        [Route("GetAanbodFromUrl")]
        [HttpGet]
        public async Task<IActionResult> GetAanbodFromUrl()
        {
            var res = await _service.FetchDataAndPopulateDB<bool>();

            if (!res)
            {
                return NotFound("There is no data");
            }

            return Ok();
        }

        // GET: api/Aanbod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aanbod>>> GetAanbod()
        {
            return await _context.Aanbod.ToListAsync();
        }

        // GET: api/Aanbod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aanbod>> GetAanbod(int id)
        {
            var aanbod = await _context.Aanbod.FindAsync(id);

            if (aanbod == null)
            {
                return NotFound();
            }

            return aanbod;
        }

        // PUT: api/Aanbod/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAanbod(int id, Aanbod aanbod)
        {
            if (id != aanbod.Id)
            {
                return BadRequest();
            }

            _context.Entry(aanbod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AanbodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Aanbod
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aanbod>> PostAanbod(Aanbod aanbod)
        {
            _context.Aanbod.Add(aanbod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAanbod", new { id = aanbod.Id }, aanbod);
        }

        // DELETE: api/Aanbod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAanbod(int id)
        {
            var aanbod = await _context.Aanbod.FindAsync(id);
            if (aanbod == null)
            {
                return NotFound();
            }

            _context.Aanbod.Remove(aanbod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AanbodExists(int id)
        {
            return _context.Aanbod.Any(e => e.Id == id);
        }
    }
}

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
using FullStackWebApp.DTOs;

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
            var res = await _service.FetchDataAndPopulateDB<bool>(_context);

            if (!res)
            {
                return NotFound("There is no data");
            }

            return Ok();
        }

        // GET: api/Aanbod/LeaderBoard/true
        //[Route("LeaderBoard")]
        [HttpGet("LeaderBoard/{tuin}")]
        public async Task<ActionResult<IEnumerable<MakelaarDTO>>> GetLeaderBoard(bool tuin)
        {
            var result = new List<MakelaarDTO>();
            if (tuin)
            {
                result = await _context.Aanbod
                    .Where(x => x.Tuin == true)
                    .GroupBy(a => new { a.MakelaarId, a.MakelaarNaam })
                    .OrderByDescending(a => a.Count())
                    .Take(10)
                    .Select(g => new MakelaarDTO
                    {
                        Id = g.Key.MakelaarId,
                        Name = g.Key.MakelaarNaam,
                        AanbodCount = g.Count()
                    })
                    .ToListAsync();
            }
            else
            {
                result = await _context.Aanbod
                    //.Where(x => x.Tuin == true)
                    .GroupBy(a => new { a.MakelaarId, a.MakelaarNaam })
                    .OrderByDescending(a => a.Count())
                    .Take(10)
                    .Select(g => new MakelaarDTO
                    {
                        Id = g.Key.MakelaarId,
                        Name = g.Key.MakelaarNaam,
                        AanbodCount = g.Count()
                    })
                    .ToListAsync();
            }

            return result;
        }

    }
}

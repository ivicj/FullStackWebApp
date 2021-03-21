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
            var res = await _service.FetchDataAndPopulateDB<bool>(_context);

            if (!res)
            {
                return NotFound("There is no data");
            }

            return Ok();
        }

    }
}

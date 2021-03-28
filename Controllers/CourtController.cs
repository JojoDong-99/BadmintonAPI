using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberAPI.Data;
using MemberAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MemberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CourtController : ControllerBase
    {
        private readonly BadmintonContext _context;
        
        public CourtController(BadmintonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Court>>> GetCourts()
        {
            return await _context.Courts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Court>> GetCourt(int id)
        {
            var court = await _context.Courts.FindAsync(id);

            if (court == null)
            {
                return NotFound();
            }

            return court;
        }

        [HttpPost]
        public async Task<ActionResult<Court>> AddCourt(Court court)
        {
            var entityEntry = _context.Courts.Add(court);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCourt (int id)
        {
            var court = await _context.Courts.FindAsync(id);
            if (court == null)
            {
                return NotFound();
            }

            _context.Courts.Remove(court);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
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
    
    public class MemberController : ControllerBase
    {
        private readonly BadmintonContext _context;
        
        public MemberController(BadmintonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> AddMember(Member member)
        {
            var entityEntry = _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteMember (int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
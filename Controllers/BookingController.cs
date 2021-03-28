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
    
    public class BookingController : ControllerBase
    {
        private readonly BadmintonContext _context;
        
        public BookingController(BadmintonContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> AddBooking(Booking booking)
        {
            var court = await _context.Courts.FindAsync(booking.CourtId);
            if (court.OpenTime.TimeOfDay > booking.StartTime.TimeOfDay || court.CloseTime.TimeOfDay < booking.EndTime.TimeOfDay) 
            {
                return BadRequest("Time is not available");
            }
            var overlappingBookings = await _context.Bookings.Where(b => b.EndTime > booking.StartTime
                                                                   || b.StartTime < booking.EndTime).AnyAsync();
            if (overlappingBookings)
            {
                return BadRequest("Time is not available");
            }
            
            var entityEntry = _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}
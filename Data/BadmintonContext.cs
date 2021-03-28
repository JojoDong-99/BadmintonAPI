using System;
using MemberAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MemberAPI.Data
{
    public class BadmintonContext : DbContext
    {
        // an empty constructor
        public BadmintonContext() {}

        // base(options) calls the base class's constructor,
        // in this case, our base class is DbContext
        public BadmintonContext(DbContextOptions<BadmintonContext> options) : base(options) {}

        // Use DbSet<Member> to query or read and 
        // write information about A Member
        public DbSet<Member> Members { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
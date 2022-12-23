using hotel.DTOs;
using hotel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel.DbContexts
{
    public class ReservroomDbContext : DbContext
    {
        public DbSet<ReservationDTO> Reservations { get; set; }

        public ReservroomDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}

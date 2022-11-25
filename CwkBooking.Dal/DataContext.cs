using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CwkBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CwkBooking.Dal
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
    }
}
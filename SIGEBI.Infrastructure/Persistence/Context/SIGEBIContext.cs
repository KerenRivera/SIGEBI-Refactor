using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Infrastructure.Persistence.Context
{
    public class SIGEBIContext : DbContext
    {
    
        public SIGEBIContext(DbContextOptions<SIGEBIContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Reservation> Reservations { get; set; }


    }
}

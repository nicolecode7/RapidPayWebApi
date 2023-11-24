using Microsoft.EntityFrameworkCore;
using RapidPayWebApi.Models;

namespace RapidPayWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }

    }
}

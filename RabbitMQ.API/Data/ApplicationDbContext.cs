using Microsoft.EntityFrameworkCore;
using RabbitMQ.API.Entities;

namespace RabbitMQ.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}

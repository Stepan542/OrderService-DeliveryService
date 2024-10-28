using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace DeliveryService.Data
{
    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options)
            : base(options) {}

        public DbSet<Order> OrderDeliveries { get; set; }
    }
}
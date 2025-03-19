using Microsoft.EntityFrameworkCore;
using Sales_Microservice.Models;

namespace Sales_Microservice.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Inventory> Inventory { get; set; }
    }
}

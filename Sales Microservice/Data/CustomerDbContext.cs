using Microsoft.EntityFrameworkCore;
using Sales_Microservice.Models;

namespace Sales_Microservice.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}

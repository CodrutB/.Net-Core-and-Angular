using CoreAngularPoC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreAngularPoC.Data
{
    public class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions<CoreContext> options) :base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}

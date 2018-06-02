using CoreAngularPoC.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreAngularPoC.Data
{
    public class CoreContext : IdentityDbContext<StoreUser>
    {
        public CoreContext(DbContextOptions<CoreContext> options) :base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}

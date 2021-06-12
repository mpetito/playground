using Microsoft.EntityFrameworkCore;

namespace Playground.Domain
{
    public class PlaygroundDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Invoice> Invoices => Set<Invoice>();

        public PlaygroundDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}

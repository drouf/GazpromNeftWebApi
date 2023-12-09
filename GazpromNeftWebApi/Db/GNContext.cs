using GazpromNeftWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GazpromNeftWebApi.Db
{
    public class GNContext : DbContext
    {
        DbSet<User> Users { get; set; }

        public GNContext(DbContextOptions<GNContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}

using GazpromNeftDomain.Entities;
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
    }
}

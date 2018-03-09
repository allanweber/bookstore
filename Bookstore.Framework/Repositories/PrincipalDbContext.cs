using Microsoft.EntityFrameworkCore;

namespace Bookstore.Framework.Repositories
{
    public class PrincipalDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PrincipalDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration<Technology>(new TechnologyMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

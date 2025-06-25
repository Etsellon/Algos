using Algos.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace Algos.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Companies = Set<CompanyEntity>();
        }

        public DbSet<CompanyEntity> Companies { get; set; }
    }
}

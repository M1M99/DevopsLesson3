using Entities;
using Microsoft.EntityFrameworkCore;

namespace DevopsLesson3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> build):base(build)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}

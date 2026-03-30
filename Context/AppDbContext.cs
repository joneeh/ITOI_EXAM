using ITOI_EXAM.Models;
using Microsoft.EntityFrameworkCore;

namespace ITOI_EXAM.Context
{

    public class AppDbContext : DbContext
    {
        public DbSet<Users> Users => Set<Users>();
        public DbSet<UserRoles> UserRoles => Set<UserRoles>();
        public DbSet<Tasks> Tasks => Set<Tasks>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}

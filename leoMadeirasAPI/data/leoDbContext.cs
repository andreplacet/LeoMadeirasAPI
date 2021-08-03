using leoMadeirasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace leoMadeirasAPI.data
{
    public class leoDbContext : DbContext
    {
        public leoDbContext(DbContextOptions<leoDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
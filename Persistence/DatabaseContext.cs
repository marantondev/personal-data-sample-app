using Microsoft.EntityFrameworkCore;
using PersonalInfoSampleApp.Model;

namespace PersonalInfoSampleApp.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        public DbSet<PersonalInfo> PersonalInfo { get; set; }
        public DbSet<City> City { get; set; }
    }
}

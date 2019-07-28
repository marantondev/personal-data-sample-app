using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalInfoSampleApp.Model;

namespace PersonalInfoSampleApp.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        public DbSet<PersonalInfo> PersonalInfo { get; set; }
        public DbSet<City> City { get; set; }

        public Address GetAddressById(int id)
        {
            return Find<Address>(id);
        }

        async Task<int> IDatabaseContext.SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
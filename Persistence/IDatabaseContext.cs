using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalInfoSampleApp.Model;

namespace PersonalInfoSampleApp.Persistence
{
    public interface IDatabaseContext
    {
        DbSet<City> City { get; set; }
        DbSet<PersonalInfo> PersonalInfo { get; set; }
        Address GetAddressById(int id);
        Task<int> SaveChangesAsync();
    }
}
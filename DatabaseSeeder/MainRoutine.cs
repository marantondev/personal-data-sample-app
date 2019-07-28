using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PersonalInfoSampleApp.Model;
using PersonalInfoSampleApp.Persistence;

namespace PersonalInfoSampleApp.DatabaseSeeder
{
    internal sealed class MainRoutine
    {
        private readonly string _databasePath;

        public MainRoutine()
        {
            _databasePath = "Server=(localdb)\\mssqllocaldb;Database=PersonalInfoSampleAppContext-8f90eb8e-6278-45c9-8a4b-810a6ea73ddd;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public MainRoutine(string databasePath)
        {
            _databasePath = databasePath;
        }

        internal void Run()
        {
            using(var dbContext = CreateDatabaseContext())
            {
                dbContext.Database.Migrate();
                var entries = DeserializeEntries();
                dbContext.AttachRange(entries);
                dbContext.SaveChanges();
            }
        }

        private City[] DeserializeEntries()
        {
            var serializedCities = Properties.Resources.Cities;
            return JsonConvert.DeserializeObject<City[]>(serializedCities);
        }

        private DatabaseContext CreateDatabaseContext()
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer(_databasePath);
            return new DatabaseContext(builder.Options);
        }
    }
}
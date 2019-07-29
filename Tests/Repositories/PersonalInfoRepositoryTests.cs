using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalInfoSampleApp.Model;
using PersonalInfoSampleApp.Persistence;
using PersonalInfoSampleApp.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalInfoSampleApp.Tests.Repositories
{
    [TestFixture]
    public sealed class PersonalInfoRepositoryTests
    {
        [Test]
        public void Constructor_NullContext_ThrowsArgumentNullException()
        {
            var sutSetup = new SutSetup()
            {
                Database = null
            };

            Assert.Throws<ArgumentNullException>(() => sutSetup.CreateSut());
        }

        [Test]
        public async Task SubmitPersonalInfo_NoDuplication_AddsRecordToDb()
        {
            var sutSetup = new SutSetup();
            var sut = sutSetup.CreateSut();

            await sut.SubmitPersonalInfo(sutSetup.UniquePersonalInfo);

            Assert.That(sutSetup.Database.PersonalInfo.Count() == 2);
        }

        [Test]
        public async Task SubmitPersonalIndo_DuplicateNameUniqueAddress_AddsRecordToDb()
        {
            var sutSetup = new SutSetup();
            var sut = sutSetup.CreateSut();

            await sut.SubmitPersonalInfo(sutSetup.DuplicateInfoWithUniqueAddress);

            Assert.That(sutSetup.Database.PersonalInfo.Count() == 2);
        }

        [Test]
        public async Task SubmitPersonalIndo_DuplicateNameAndAddress_NothingHappens()
        {
            var sutSetup = new SutSetup();
            var sut = sutSetup.CreateSut();

            await sut.SubmitPersonalInfo(sutSetup.DuplicateInfo);

            Assert.That(sutSetup.Database.PersonalInfo.Count() == 1);
        }
        private sealed class SutSetup
        {
            public IDatabaseContext Database { get; set; }
            public PersonalInfo DuplicateInfo { get; }
            public Address DuplicateAddress { get; } = new Address()
            {
                CityId = 1,
                Street = "Test",
                ResidenceNumber = "1",
                PostalNumber = "00-000"
            };

            public PersonalInfo UniquePersonalInfo { get; } = new PersonalInfo()
            {
                FirstName = "Test1",
                LastName = "Test2",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ResidenceAddress = new Address()
                {
                    CityId = 1,
                    Street = "Test3",
                    ResidenceNumber = "4",
                    PostalNumber = "00-000"
                },
                UseSameAddress = true
            };

            public PersonalInfo DuplicateInfoWithUniqueAddress { get; } = new PersonalInfo()
            {
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = DateTime.Parse("1980-01-01"),
                ResidenceAddress = new Address()
                {
                    CityId = 1,
                    Street = "Test3",
                    ResidenceNumber = "4",
                    PostalNumber = "00-000"
                },
                UseSameAddress = true
            };

            public SutSetup()
            {
                DuplicateInfo = new PersonalInfo()
                {
                    FirstName = "Test",
                    LastName = "Test",
                    DateOfBirth = DateTime.Parse("1980-01-01"),
                    ResidenceAddressId = 1,
                    ResidenceAddress = DuplicateAddress
                };
                Database = CreateDatabase();
            }

            public PersonalInfoRepository CreateSut()
            {
                return new PersonalInfoRepository(Database);
            }
            private IDatabaseContext CreateDatabase()
            {
                //Appending a Guid to the name ensures we get a fresh database for every test
                var dbOptions = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseInMemoryDatabase("PersonalInfoTest"+Guid.NewGuid().ToString()).Options;
                var db = new DatabaseContext(dbOptions);
                db.PersonalInfo.Add(DuplicateInfo);
                db.SaveChanges();
                return db;
            }
        }
    }
}
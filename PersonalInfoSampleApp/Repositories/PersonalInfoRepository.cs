using PersonalInfoSampleApp.Model;
using PersonalInfoSampleApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalInfoSampleApp.Repositories
{
    public sealed class PersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly IDatabaseContext _context;

        public PersonalInfoRepository(IDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task SubmitPersonalInfo(PersonalInfo personalInfo)
        {
            if(!IsDuplicate(personalInfo))
            {
                _context.PersonalInfo.Add(personalInfo);
                await _context.SaveChangesAsync();
            }
            return;
        }

        private bool IsDuplicate(PersonalInfo personalInfo)
        {
            var potentialDuplicates = _context.PersonalInfo.Where(p => HasSameInfo(personalInfo, p));
            foreach(var potentialDuplicate in potentialDuplicates)
            {
                var address = _context.GetAddressById(potentialDuplicate.ResidenceAddressId);
                if(IsSameAddress(personalInfo.ResidenceAddress, address))
                    return true;
            }
            return false;
        }

        private bool IsSameAddress(Address inputAddress, Address databaseEntry)
        {
            return inputAddress.CityId == databaseEntry.CityId
                && inputAddress.Street == databaseEntry.Street
                && inputAddress.ResidenceNumber == databaseEntry.ResidenceNumber;
        }

        private bool HasSameInfo(PersonalInfo inputPerson, PersonalInfo databaseEntry)
        {
            return inputPerson.FirstName == databaseEntry.FirstName
                && inputPerson.LastName == databaseEntry.LastName
                && inputPerson.DateOfBirth == databaseEntry.DateOfBirth;
        }

    }
}

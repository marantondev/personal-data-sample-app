using PersonalInfoSampleApp.Model;
using PersonalInfoSampleApp.Pages.Form.ViewModel;
using PersonalInfoSampleApp.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalInfoSampleApp.Pages.Form.Handlers
{
    public sealed class SubmitPersonalInfoCommandHandler
    {
        private readonly DatabaseContext _context;

        public SubmitPersonalInfoCommandHandler(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Execute(PersonalInfoViewModel dataInput)
        {
            PersonalInfo personalInfo = GetPersonalInfoModel(dataInput);
            if(!IsDuplicate(personalInfo))
            {
                _context.PersonalInfo.Add(personalInfo);
                await _context.SaveChangesAsync();
            }
            return;
        }

        private PersonalInfo GetPersonalInfoModel(PersonalInfoViewModel viewModel)
        {
            return new PersonalInfo()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                DateOfBirth = viewModel.DateOfBirth,
                ResidenceAddress = GetAddressModel(viewModel.ResidenceAddress),
                UseSameAddress = viewModel.UseSameAddress,
                CorrespondenceAddress = GetAddressModel(viewModel.CorrespondenceAddress)
            };
        }

        private bool IsDuplicate(PersonalInfo personalInfo)
        {
            var potentialDuplicates = _context.PersonalInfo.Where(p => HasSameInfo(personalInfo, p));
            foreach(var potentialDuplicate in potentialDuplicates)
            {
                var address = _context.Find<Address>(potentialDuplicate.ResidenceAddressId);
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

        private Address GetAddressModel(AddressViewModel viewModel)
        {
            if(viewModel is null)
                return null;
            else
                return new Address()
                {
                    CityId = viewModel.CityId,
                    Street = viewModel.Street,
                    ResidenceNumber = viewModel.ResidenceNumber,
                    PostalNumber = viewModel.PostalNumber
                };
        }
    }
}
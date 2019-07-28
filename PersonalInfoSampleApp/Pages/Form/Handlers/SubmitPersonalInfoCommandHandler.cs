using PersonalInfoSampleApp.Model;
using PersonalInfoSampleApp.Pages.Form.ViewModel;
using PersonalInfoSampleApp.Repositories;
using System.Threading.Tasks;

namespace PersonalInfoSampleApp.Pages.Form.Handlers
{
    public sealed class SubmitPersonalInfoCommandHandler
    {
        private readonly IPersonalInfoRepository _repository;

        public SubmitPersonalInfoCommandHandler(IPersonalInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(PersonalInfoViewModel dataInput)
        {
            PersonalInfo personalInfo = GetPersonalInfoModel(dataInput);
            await _repository.SubmitPersonalInfo(personalInfo);
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
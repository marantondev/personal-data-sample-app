using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalInfoSampleApp.Model;
using PersonalInfoSampleApp.Pages.Form.ViewModel;
using PersonalInfoSampleApp.Persistence;

namespace PersonalInfoSampleApp.Pages.Form
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _context;

        public CreateModel(DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PersonalInfoViewModel PersonalInfo { get; set; }

        public List<SelectListItem> Cities { get; private set; }

        public IActionResult OnGet()
        {
            Cities = _context.City.Select(p => new SelectListItem(p.CityName, p.Id.ToString())).OrderBy(p=>p.Text).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _context.PersonalInfo.Add(new PersonalInfo()
            {
                FirstName = PersonalInfo.FirstName,
                LastName = PersonalInfo.LastName,
                DateOfBirth = PersonalInfo.DateOfBirth,
                ResidenceAddress = GetAddressModel(PersonalInfo.ResidenceAddress),
                UseSameAddress = PersonalInfo.UseSameAddress,
                CorrespondenceAddress = GetAddressModel(PersonalInfo.ResidenceAddress)
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
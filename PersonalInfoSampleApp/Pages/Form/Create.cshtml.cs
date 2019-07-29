using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalInfoSampleApp.Pages.Form.Handlers;
using PersonalInfoSampleApp.Pages.Form.ViewModel;

namespace PersonalInfoSampleApp.Pages.Form
{
    public class CreateModel : PageModel
    {
        private readonly IGetCityListQueryHandler _getCitiesQuery;
        private readonly ISubmitPersonalInfoCommandHandler _submitCommandHandler;

        public CreateModel(
            IGetCityListQueryHandler getCitiesQuery,
            ISubmitPersonalInfoCommandHandler submitCommandHandler)
        {
            _getCitiesQuery = getCitiesQuery;
            _submitCommandHandler = submitCommandHandler;
        }

        [BindProperty]
        public PersonalInfoViewModel PersonalInfo { get; set; }

        public List<SelectListItem> Cities { get; private set; }

        public IActionResult OnGet()
        {
            Cities = _getCitiesQuery.Execute();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            await _submitCommandHandler.Execute(PersonalInfo);

            return RedirectToPage("ThankYou");
        }
    }
}
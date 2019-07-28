using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalInfoSampleApp.Pages.Form.Handlers;
using PersonalInfoSampleApp.Pages.Form.ViewModel;
using PersonalInfoSampleApp.Persistence;

namespace PersonalInfoSampleApp.Pages.Form
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly SubmitPersonalInfoCommandHandler _submitCommandHandler;

        public CreateModel(DatabaseContext context, SubmitPersonalInfoCommandHandler submitCommandHandler)
        {
            _context = context;
            _submitCommandHandler = submitCommandHandler;
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

            await _submitCommandHandler.Execute(PersonalInfo);

            return RedirectToPage("/Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalInfoSampleApp.Model;
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
        public PersonalInfo PersonalInfo { get; set; }

        public List<SelectListItem> Cities { get; private set; }

        public IActionResult OnGet()
        {
            Cities = _context.City.Select(p => new SelectListItem(p.CityName, p.Id.ToString())).ToList().OrderBy(p=>p.Text, StringComparer.CurrentCulture).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            _context.PersonalInfo.Add(PersonalInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
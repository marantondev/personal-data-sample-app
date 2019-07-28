using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalInfoSampleApp.Model;
using PersonalInfoSampleApp.Persistence;

namespace PersonalInfoSampleApp.Pages.Form
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _context;

        public IndexModel(DatabaseContext context)
        {
            _context = context;
        }

        public IList<PersonalInfo> PersonalInfo { get;set; }

        public async Task OnGetAsync()
        {
            PersonalInfo = await _context.PersonalInfo.ToListAsync();
        }
    }
}

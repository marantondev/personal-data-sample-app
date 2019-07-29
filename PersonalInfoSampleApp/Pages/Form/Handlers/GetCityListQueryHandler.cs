using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalInfoSampleApp.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace PersonalInfoSampleApp.Pages.Form.Handlers
{
    public class GetCityListQueryHandler : IGetCityListQueryHandler
    {
        private readonly IDatabaseContext _context;
        public GetCityListQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Execute()
        {
            return _context.City.Select(p => new SelectListItem(p.CityName, p.Id.ToString())).OrderBy(p => p.Text).ToList();
        }
    }
}
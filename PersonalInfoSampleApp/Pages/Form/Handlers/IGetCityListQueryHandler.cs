using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PersonalInfoSampleApp.Pages.Form.Handlers
{
    public interface IGetCityListQueryHandler
    {
        List<SelectListItem> Execute();
    }
}
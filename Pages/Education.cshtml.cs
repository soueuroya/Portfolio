using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages
{
    public class EducationModel : PageModel
    {
        private readonly ILogger<EducationModel> _logger;

        public EducationModel(ILogger<EducationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}

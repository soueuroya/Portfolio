using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages
{
    public class ExperienceModel : PageModel
    {
        private readonly ILogger<ExperienceModel> _logger;

        public ExperienceModel(ILogger<ExperienceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}

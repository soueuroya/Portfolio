using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages
{
    public class AssetsModel : PageModel
    {
        private readonly ILogger<AssetsModel> _logger;

        public AssetsModel(ILogger<AssetsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}

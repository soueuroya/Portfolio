using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Pages
{
    public class GamesModel : PageModel
    {
        private readonly ILogger<GamesModel> _logger;

        public GamesModel(ILogger<GamesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}

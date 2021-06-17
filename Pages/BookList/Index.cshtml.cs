using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContent _db;
        private IWebHostEnvironment _env;
        public IndexModel(ApplicationDbContent db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            if (book.Format != null && book.Format != "")
            {
                var dir = _env.ContentRootPath + "/wwwroot/Images/";
                string path = dir + book.Id.ToString() + book.Format;
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }

            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostBuy(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            book.Cart = true;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
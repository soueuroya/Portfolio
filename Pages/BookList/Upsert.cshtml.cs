using System.IO;
using System.Threading.Tasks;
using Portfolio.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace Portfolio.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContent _db;
        private IWebHostEnvironment _env;
        public UpsertModel(ApplicationDbContent db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [BindProperty]
        public Book Book { get; set; }
        
        public async Task<IActionResult> OnGet(int? id){
            Book = new Book();
            if (id == null)
            {
                return Page();
            }
            Book = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        public static async Task<byte[]> GetBytes([FromForm] IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<IActionResult> OnPost(IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    var dir = _env.ContentRootPath + "/wwwroot/Images/";
                    if (image != null)
                    {
                        var extension = Path.GetExtension(image.FileName);
                        using (var fileStream = new FileStream(Path.Combine(dir, "temp" + extension), FileMode.Create, FileAccess.Write))
                        {
                            image.CopyTo(fileStream);
                        }
                        Book.Image = new byte[image.Length];
                        Book.Image = await GetBytes(image);
                        Book.Format = extension;
                    }

                    _db.Book.Add(Book);
                    await _db.SaveChangesAsync();

                    if (image != null)
                    {
                        System.IO.File.Move(Path.Combine(dir, "temp" + Book.Format), Path.Combine(dir, Book.Id.ToString() + Book.Format));
                    }
                }
                else
                {
                    var dir = _env.ContentRootPath + "/wwwroot/Images/";
                    if (image != null)
                    {
                        var extension = Path.GetExtension(image.FileName);
                        using (var fileStream = new FileStream(Path.Combine(dir, "temp" + extension), FileMode.Create, FileAccess.Write))
                        {
                            image.CopyTo(fileStream);
                        }
                        Book.Image = new byte[image.Length];
                        Book.Image = await GetBytes(image);
                        Book.Format = extension;
                    }

                    _db.Book.Update(Book);
                    await _db.SaveChangesAsync();

                    if (image != null)
                    {
                        string path = Path.Combine(dir, Book.Id.ToString() + Book.Format);
                        FileInfo file = new FileInfo(path);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                        System.IO.File.Move(Path.Combine(dir, "temp" + Book.Format), path);
                    }
                }
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
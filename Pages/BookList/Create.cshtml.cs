using System.Threading.Tasks;
using Portfolio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Portfolio.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContent _db;
        private IHostingEnvironment _env;
        public CreateModel(ApplicationDbContent db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        [BindProperty]
        public Book Book { get; set; }

        public static async Task<byte[]> GetBytes([FromForm]IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<IActionResult> OnPost(IFormFile image)
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

            if(ModelState.IsValid)
            {
                await _db.Book.AddAsync(Book);
                await _db.SaveChangesAsync();
                
                if (image != null)
                {
                    System.IO.File.Move(Path.Combine(dir, "temp" + Book.Format), Path.Combine(dir, Book.Id.ToString() + Book.Format));
                }
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}

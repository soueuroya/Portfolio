using Portfolio.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Portfolio.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContent _db;
        private IHostingEnvironment _env;
        public BookController(ApplicationDbContent db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.Where(b => b.Deleted == false).ToListAsync() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Book not Found." });
            }
            //_db.Book.Remove(bookFromDb);
            if (bookFromDb.Format != null && bookFromDb.Format != "")
            {
                var dir = _env.ContentRootPath + "/wwwroot/Images/";
                string path = dir + bookFromDb.Id.ToString() + bookFromDb.Format;
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            bookFromDb.Deleted = true;
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        [HttpPost]
        public async Task<IActionResult> Action(int id, string a)
        {
            string msg = "";
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Book not Found." });
            }

            switch (a)
            {
                case "b":
                    msg = "Added to cart";
                    bookFromDb.Cart = true;
                    break;

                case "r":
                    bookFromDb.Cart = false;
                    msg = "Removed from cart";
                    break;

                case "c":
                    bookFromDb.Cart = false;
                    bookFromDb.Bought = true;
                    msg = "Transaction Completed";
                    break;

                default:
                    break;
            }

            await _db.SaveChangesAsync();
            return Json(new { success = true, message = msg });
        }
    }
}
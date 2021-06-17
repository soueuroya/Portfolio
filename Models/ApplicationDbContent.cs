using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ApplicationDbContent : DbContext
    {
        public ApplicationDbContent(DbContextOptions<ApplicationDbContent> options) : base(options)
        {

        }
        public DbSet<Book> Book { get; set; }
    }
}

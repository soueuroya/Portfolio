using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public int ReleaseYear { get; set; }

        public bool Cart { get; set; }

        public bool Bought { get; set; }

        public bool Deleted { get; set; }

        public byte[] Image { get; set; }

        public string Format { get; set; }
    }
}

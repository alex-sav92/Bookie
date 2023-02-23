using System.ComponentModel.DataAnnotations;

namespace Bookie.DirectApp.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters on Author name")]
        public string Name { get; set; }
    }
}

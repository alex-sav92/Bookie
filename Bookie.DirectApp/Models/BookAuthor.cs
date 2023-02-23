using Microsoft.EntityFrameworkCore;

namespace Bookie.DirectApp.Models
{
    [PrimaryKey(nameof(BookId), nameof(AuthorId))]
    public class BookAuthor
    {
        public int BookId { get; set; }

        public int AuthorId { get; set; }
        public int DisplayOrder { get; set; }

        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}

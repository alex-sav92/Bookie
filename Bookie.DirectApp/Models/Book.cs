using System.ComponentModel.DataAnnotations;

namespace Bookie.DirectApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(300, ErrorMessage = "Title must be maximum 300 characters long")]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishedOn { get; set; }

        public string Publisher { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        //relationships
        public PriceOffer? Promotion { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<BookAuthor>? AuthorsLink { get; set; }
    }
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
    }
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class Review
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public string VoterName { get; set; }
        public int NumStars { get; set; }
        public string Comment { get; set; }
    }
    public class PriceOffer
    {
        public int PriceOfferId { get; set; }
        public decimal NewPrice { get; set; }
        public string PromotionalText { get; set; }
        public int BookId { get; set; }
    }
}

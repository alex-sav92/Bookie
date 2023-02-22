using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookie.DirectApp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        [MaxLength(300, ErrorMessage = "Title must be maximum 300 characters long")]
        public string Title { get; set; }


        [MaxLength(500, ErrorMessage = "Title must be maximum 300 characters long")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
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

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters on Author name")]
        public string Name { get; set; }
    }

    [PrimaryKey(nameof(BookId), nameof(AuthorId))]
    public class BookAuthor
    {
        public int BookId { get; set; }

        public int AuthorId { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class Review
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }

        [Required]
        public string VoterName { get; set; }

        [Required]
        [Range(1, 5)]
        public int NumStars { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }
    }
    public class PriceOffer
    {
        public int PriceOfferId { get; set; }

        [Required]
        public decimal NewPrice { get; set; }

        [Required]
        public string PromotionalText { get; set; }

        [Required]
        public int BookId { get; set; }
    }

    public class BookViewModel 
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Authors { get; set; }
        public string Price { get; set; }
        public string PublishedOn { get; set; }
        public string ImageUrl { get; set; }
        public string Publisher { get; set; }
    }

    public class AddAuthor 
    {
        public int BookId { get; set; }
        public string AuthorName { get; set; }
    }

    public static class Extensions 
    {
        public static BookViewModel ToViewModel(this Book book) 
        {
            var vm = new BookViewModel();
            vm.Title = book.Title;
            vm.Description = book.Description;
            vm.Price = book.Price.ToString("##.## $");
            vm.PublishedOn = book.PublishedOn.Date.ToShortDateString();
            vm.Publisher = book.Publisher;
            vm.BookId = book.BookId;
            vm.ImageUrl = book.ImageUrl;

            return vm;
        }
    }
}

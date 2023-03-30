using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Data.Entities
{
    [Index(nameof(Title))]
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
}

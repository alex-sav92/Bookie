using System.ComponentModel.DataAnnotations;

namespace Bookie.DirectApp.Models
{
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
}

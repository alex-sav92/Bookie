using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Data.Entities
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

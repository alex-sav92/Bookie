using System.ComponentModel.DataAnnotations;

namespace Bookie.DirectApp.Models
{
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

        public virtual Book Book { get; set; }
    }
}

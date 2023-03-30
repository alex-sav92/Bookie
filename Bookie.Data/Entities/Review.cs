using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Data.Entities
{
    [Index(nameof(Comment), Name = "IX_ReviewComment", IsUnique = true)]
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

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bookie.DirectApp.Models
{
    [PrimaryKey(nameof(UserEmail), nameof(BookId))]
    public class Favorite
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}

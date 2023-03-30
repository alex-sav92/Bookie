using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Data.Entities
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Data.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters on Author name")]
        public string Name { get; set; }
    }
}

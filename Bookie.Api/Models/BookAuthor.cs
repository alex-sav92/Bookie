﻿namespace Bookie.Api.Models
{
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public int DisplayOrder { get; set; }
    }
}

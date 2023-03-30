using BenchmarkDotNet.Attributes;
using Bookie.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF
{
    public static class ExtensionsNPlusOne
    {
        public static void NPlus1Problem(this BookieDbContext _db)
        {
            var books = _db.Books.Where(b => b.BookId > 520).ToList();

            foreach (var book in books)
            {
                _db.Entry(book).Collection(b => b.Reviews).Load();

                var reviews = book.Reviews
                    .Where(r => !string.IsNullOrEmpty(r.Comment))
                    .ToList();
            }
        }

        public static void NPlus1Solution1(this BookieDbContext _db)
        {
            var books = _db.Books
                .Where(b => b.BookId > 520)
                .Include(b => b.Reviews)
                .ToList();

            //this will not generate extra queries:
            foreach (var book in books)
            {
                var avg = book.Reviews.Average(r => r.NumStars);
            }
        }

        public static void NPlus1Solution2(this BookieDbContext _db)
        {
            var books = _db.Books
                .Where(b => b.BookId > 520)
                .Select(o => new
                {
                    Reviews = o.Reviews
                        .Where(r => !string.IsNullOrEmpty(r.Comment))
                        .ToList()
                })
                .ToList();

        }


    }
}

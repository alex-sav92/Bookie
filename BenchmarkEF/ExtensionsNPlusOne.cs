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
        public static double NPlus1Problem(this BookieDbContext _db)
        {
            double avgReviews = 0;
            int numReviews = 0;
            var books = _db.Books.Where(b => b.BookId > 520).ToList();

            foreach (var book in books)
            {
                _db.Entry(book).Collection(b => b.Reviews).Load();

                if (book.Reviews != null)
                {
                    foreach (var review in book.Reviews)
                    {
                        avgReviews += review.NumStars;
                        numReviews++;
                    }
                }
            }
            return avgReviews / numReviews;
        }

        public static double NPlus1Solution1(this BookieDbContext _db)
        {
            double avgReviews = 0;
            int numReviews = 0;
            var books = _db.Books
                .Where(b => b.BookId > 520)
                .Include(b => b.Reviews)
                .ToList();

            //this will not generate extra queries:
            foreach (var book in books)
            {
                foreach (var review in book.Reviews) 
                {
                    avgReviews += review.NumStars;
                    numReviews++;
                }
            }
            return avgReviews / numReviews;
        }

        public static double NPlus1Solution2(this BookieDbContext _db)
        {
            var books = _db.Books
                .Where(b => b.BookId > 520)
                .Select(o => new
                {
                    AvgReview = o.Reviews != null ? o.Reviews.Average(r => r.NumStars) : 0
                }).ToList();

            var avg = books.Sum(b => b.AvgReview) / books.Count;
            return avg;
        }


    }
}

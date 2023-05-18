using BenchmarkDotNet.Attributes;
using Bookie.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF.Tests
{
    [MemoryDiagnoser]
    public class SplitQueries
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
        }

        [Benchmark]
        public void SplitQuery()
        {
            var books = _db.Books
                .AsSplitQuery()
                .Include(b => b.Reviews)
                .Include(b => b.AuthorsLink)
                .Where(b => b.BookId > 520);

        }

        [Benchmark]
        public void NoSplitQuery()
        {
            var books = _db.Books
                .Include(b => b.Reviews)
                .Include(b => b.AuthorsLink)
                .Where(b => b.BookId > 520);

        }
    }
}

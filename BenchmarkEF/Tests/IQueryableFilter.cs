using BenchmarkDotNet.Attributes;
using Bookie.Data;
using Bookie.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF
{
    [MemoryDiagnoser]
    public class IQueryableFilter
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
        }

        [Benchmark]
        public void IEnumerable()
        {
            IEnumerable<Book> books = _db.Books;
            var filter = books.Where(x => x.BookId >= 2);
            var results = filter.ToList();
        }

        [Benchmark]
        public void IQueryable()
        {
            var books = _db.Books.AsQueryable();
            var filter = books.Where(b => b.BookId >= 2);
            var results = filter.ToList();
        }
    }
}

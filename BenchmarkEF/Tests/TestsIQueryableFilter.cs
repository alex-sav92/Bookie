using BenchmarkDotNet.Attributes;
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
    public class TestsIQueryableFilter
    {
        private BookieDbContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var _options = new DbContextOptionsBuilder<BookieDbContext>()
                .LogTo(Console.WriteLine)
                .UseSqlServer("Data Source=bookie-server.database.windows.net;Initial Catalog=bookieDB;User ID=alex-bookie;Password=1-q-a-z-;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options;
            _db = new BookieDbContext(_options);
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

using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF
{
    public class TestsPaging
    {
        private BookieDbContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var _options = new DbContextOptionsBuilder<BookieDbContext>()
                .UseSqlServer("Data Source=bookie-server.database.windows.net;Initial Catalog=bookieDB;User ID=alex-bookie;Password=1-q-a-z-;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options;
            _db = new BookieDbContext(_options);
        }

        [Benchmark]
        public int WithPaging20()
        {
            var books = _db.Books.Take(20).ToList();

            return books.Count;
        }

        [Benchmark]
        public int WithPaging50()
        {
            var books = _db.Books.Take(50).ToList();

            return books.Count;
        }

        [Benchmark]
        public int WithoutPaging()
        {
            var books = _db.Books.ToList();

            return books.Count;
        }
    }
}

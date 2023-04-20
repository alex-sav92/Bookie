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
    public class TestsTracking
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

        [Benchmark(Baseline = true)]
        public List<Book> AsTracking()
        {
            return _db.Books.ToList();
        }

        [Benchmark]
        public List<Book> AsNoTracking()
        {
            return _db.Books.AsNoTracking().ToList();
        }
    }
}

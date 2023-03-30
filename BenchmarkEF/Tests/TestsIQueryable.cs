using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF
{
    [MemoryDiagnoser]
    public class TestsIQueryable
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
        public int UseIEnumerable()
        {
            var count = _db.Books.ToList().Count();
            return count;
        }

        [Benchmark]
        public int UseIQueryable()
        {
            var count = _db.Books.Count();
            return count;
        }
    }
}

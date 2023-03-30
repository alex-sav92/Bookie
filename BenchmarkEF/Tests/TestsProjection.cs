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
    public class TestsProjection
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
        public int ProjectOnlyTwoFields()
        {
            var books = _db.Books.Select(b => 
                new 
                { 
                    Desc = b.Description, 
                    Name = b.Title 
                }).ToList();

            return books.Count;
        }

        [Benchmark]
        public int ReadAllFields()
        {
            var books = _db.Books.ToList();

            return books.Count;
        }
    }
}

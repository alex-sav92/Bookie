using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF.Tests
{
    [MemoryDiagnoser]
    public class CompiledQueries
    {
        private BookieDbContext _db;
        private string[] Top5Publishers = new string[] { 
            "Manning", "Nemira", "O'Reilly Media", "Penguin", "Little, Brown and Company" };

        [GlobalSetup]
        public void GlobalSetup()
        {
            var _options = new DbContextOptionsBuilder<BookieDbContext>()
                .UseSqlServer("Data Source=bookie-server.database.windows.net;Initial Catalog=bookieDB;User ID=alex-bookie;Password=1-q-a-z-;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options;
            _db = new BookieDbContext(_options);
        }

        [Benchmark]
        public void TopFivePublishersCompiledQuery()
        {
            var books = _db.GetBooksByPublisher(Top5Publishers[0]);
            books = _db.GetBooksByPublisher(Top5Publishers[1]);
            books = _db.GetBooksByPublisher(Top5Publishers[2]);
            books = _db.GetBooksByPublisher(Top5Publishers[3]);
            books = _db.GetBooksByPublisher(Top5Publishers[4]);
        }

        [Benchmark]
        public void TopFivePublishersIndependentQuery()
        {
            var books = _db.Books.Where(b => b.Publisher == Top5Publishers[0]);
            books = _db.Books.Where(b => b.Publisher == Top5Publishers[1]);
            books = _db.Books.Where(b => b.Publisher == Top5Publishers[2]);
            books = _db.Books.Where(b => b.Publisher == Top5Publishers[3]);
            books = _db.Books.Where(b => b.Publisher == Top5Publishers[4]);
        }
    }
}

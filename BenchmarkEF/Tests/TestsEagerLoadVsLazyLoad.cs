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
    public class TestsEagerLoadVsLazyLoad
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
        public void LazyLoadAllRelatedEntities()
        {
            var books = _db.Books;
            var list = books.ToList();
        }

        [Benchmark]
        public void EagerLoadOnlyReviews()
        {
            _db.ChangeTracker.LazyLoadingEnabled = false;
            var books = _db.Books.Include(b => b.Reviews);
        }

        [Benchmark]
        public void EagerLoadOnlyAuthors()
        {
            _db.ChangeTracker.LazyLoadingEnabled = false;
            var books = _db.Books.Include(b => b.AuthorsLink);
        }

        [Benchmark]
        public void EagerLoadAllRelatedEntities()
        {
            _db.ChangeTracker.LazyLoadingEnabled = false;
            var books = _db.Books
                .Include(b => b.AuthorsLink)
                .Include(b => b.Reviews);
        }
    }
}

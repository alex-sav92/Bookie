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
    [MemoryDiagnoser]
    public class EagerLoadVsLazyLoad
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
        }

        [Benchmark]
        public void LazyLoadAllRelatedEntities()
        {
            var books = _db.Books;
            
            foreach(var b in books)
            {
                if (b.Reviews != null)
                    foreach (var r in b.Reviews) { }
                if (b.AuthorsLink != null)
                    foreach (var a in b.AuthorsLink) { }
            }
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

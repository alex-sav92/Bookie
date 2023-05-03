using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookie.Data.Entities;
using Bookie.Data;

namespace BenchmarkEF
{
    [MemoryDiagnoser]
    public class ContextPooling
    {
        private DbContextOptions<BookieDirectAppContext> _options;
        private PooledDbContextFactory<BookieDirectAppContext> _poolingFactory;


        [GlobalSetup]
        public void Setup()
        {
            using var db = new BookieDirectAppContext();
            //context.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            _poolingFactory = new PooledDbContextFactory<BookieDirectAppContext>(_options);
        }

        [Benchmark]
        public void WithoutContextPooling()
        {
            for (int i = 0; i < 10; i++)
            {
                var context = new BookieDirectAppContext();
                var book = context.Books.Take(1).ToList();
            }
        }

        [Benchmark]
        public void WithContextPooling()
        {
            for (int i = 0; i < 10; i++)
            {
                var context = _poolingFactory.CreateDbContext();
                var book = context.Books.Take(1).ToList();
            }
        }
    }
}

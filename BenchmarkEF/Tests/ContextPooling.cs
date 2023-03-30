using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookie.Data.Entities;

namespace BenchmarkEF
{
    [MemoryDiagnoser]
    public class ContextPooling
    {
        private DbContextOptions<BookieDbContext> _options;
        private PooledDbContextFactory<BookieDbContext> _poolingFactory;


        [GlobalSetup]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<BookieDbContext>()
                .UseSqlServer("Data Source=bookie-server.database.windows.net;Initial Catalog=bookieDB;User ID=alex-bookie;Password=1-q-a-z-;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options;

            using var db = new BookieDbContext(_options);
            //context.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            _poolingFactory = new PooledDbContextFactory<BookieDbContext>(_options);
        }

        [Benchmark]
        public void WithoutContextPooling()
        {
            for (int i = 0; i < 10; i++)
            {
                var context = new BookieDbContext(_options);
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

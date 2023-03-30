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
    public class TestsAverageBookPrice
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
        public double AverageBookPrice()
        {
            decimal sum = 0;
            int count = 0;
            foreach (var book in _db.Books)
            {
                sum += book.Price;
                count++;
            }

            return (double)sum / count;
        }

        [Benchmark]
        public double AverageBookPrice_NoTracking()
        {
            decimal sum = 0;
            int count = 0;
            foreach (var book in _db.Books.AsNoTracking())
            {
                sum += book.Price;
                count++;
            }

            return (double)sum / count;
        }

        [Benchmark]
        public double AverageBookPrice_ProjectOnlyFieldOfInterest()
        {
            decimal sum = 0;
            int count = 0;
            foreach (var price in _db.Books.Select(b => b.Price))
            {
                sum += price;
                count++;
            }

            return (double)sum / count;
        }

        [Benchmark(Baseline = true)]
        public decimal AverageBookPrice_CalculateInDatabase()
        {
            return _db.Books.Average(b => b.Price);
        }
    }
}

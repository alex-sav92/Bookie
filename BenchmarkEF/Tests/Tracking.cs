using BenchmarkDotNet.Attributes;
using Bookie.Data;
using Bookie.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEF
{
    [MemoryDiagnoser]
    public class Tracking
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
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

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
    public class IQueryableTests
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
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

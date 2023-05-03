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
    public class Paging
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _db = new BookieDirectAppContext();
        }

        [Benchmark]
        public int WithPaging20()
        {
            var books = _db.Books.Take(20).ToList();

            return books.Count;
        }

        [Benchmark]
        public int WithPaging50()
        {
            var books = _db.Books.Take(50).ToList();

            return books.Count;
        }

        [Benchmark]
        public int WithoutPaging()
        {
            var books = _db.Books.ToList();

            return books.Count;
        }
    }
}

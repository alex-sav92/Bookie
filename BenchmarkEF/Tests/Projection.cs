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
    public class Projection
    {
        private BookieDirectAppContext _db;

        [GlobalSetup]
        public void GlobalSetup() 
        {
            _db = new BookieDirectAppContext();
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
